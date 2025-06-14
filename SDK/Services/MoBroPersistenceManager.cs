﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Json;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Implementation of <see cref="IMoBroPersistenceManager"/> for local testing.
/// </summary>
public sealed class MoBroPersistenceManager : IMoBroPersistenceManager
{
  private const string IndexFileName = "index.json";

  private readonly ILogger _logger;
  private readonly string _indexFile;
  private readonly string _storagePath;
  private readonly JsonSerializerOptions _serializerOptions;

  private readonly Dictionary<string, string> _index;
  private readonly Dictionary<string, object?> _dataCache;

  /// <summary>
  /// Instantiates a new MoBroPersistenceManager
  /// </summary>
  /// <param name="storagePath">The path to the storage directory</param>
  /// <param name="logger">An instance of <see cref="ILogger"/></param>
  public MoBroPersistenceManager(string storagePath, ILogger logger)
  {
    Directory.CreateDirectory(storagePath);
    _logger = logger;
    _storagePath = storagePath;
    _indexFile = Path.Combine(storagePath, IndexFileName);
    _index = IndexFromFile();
    _dataCache = new Dictionary<string, object?>();
    _serializerOptions = new JsonSerializerOptions
    {
      Converters = { new MetricValueConverter() }
    };
    Cleanup();
  }

  /// <inheritdoc />
  public void Put<T>(string key, T data)
  {
    Guard.Against.Null(data);
    Guard.Against.NullOrEmpty(key);

    var entry = CreateEntry(key, data);
    if (!_index.TryGetValue(key, out var fileName))
    {
      lock (_indexFile)
      {
        fileName = GenerateFileName();
        _index[key] = fileName;
        WriteIndex();
      }
    }

    try
    {
      WriteFile(DataFilePath(fileName), JsonSerializer.Serialize(entry, _serializerOptions));
      _dataCache[key] = data;
    }
    catch (Exception)
    {
      _logger.LogWarning("Failed to serialize data for key: {Key}", key);
      if (!File.Exists(DataFilePath(fileName)))
      {
        _index.Remove(key);
      }

      throw;
    }

    _logger.LogDebug("Persisted data for key: {Key}", key);
  }

  /// <inheritdoc />
  public T? Get<T>(string key)
  {
    if (!_index.TryGetValue(key, out var fileName)) return default;

    if (_dataCache.TryGetValue(key, out var cachedObject) && cachedObject != null)
    {
      if (cachedObject.GetType() == typeof(T))
      {
        return (T?)cachedObject;
      }

      _logger.LogWarning(
        "Invalid type {StoredType} of stored data for key: {Key}. Type {RequestedType} requested",
        cachedObject.GetType(), typeof(T), key
      );
      return default;
    }

    try
    {
      var entry = JsonSerializer.Deserialize<StorageEntry>(File.ReadAllText(DataFilePath(fileName)),
        _serializerOptions);
      if (entry == null) return default;
      if (entry.Type.Equals(typeof(T).FullName))
      {
        var data = JsonSerializer.Deserialize<T>(entry.Data, _serializerOptions);
        _dataCache[key] = data;
        return data;
      }

      _logger.LogWarning(
        "Can not deserialize stored data of type {StoredType} to {RequestedType} for key: {Key}",
        entry.Type, typeof(T).FullName, key
      );
      return default;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Failed to deserialize persisted data for key: {Key}", key);
      return default;
    }
  }

  /// <inheritdoc />
  public bool Remove(string key)
  {
    _dataCache.Remove(key);

    if (!_index.Remove(key, out var fileName)) return false;

    lock (_indexFile)
    {
      WriteIndex();
      File.Delete(DataFilePath(fileName));
    }

    _logger.LogDebug("Deleted persisted data for key: {Key}", key);
    return true;
  }

  /// <inheritdoc />
  public bool Exists(string key) => _index.ContainsKey(key);

  /// <inheritdoc />
  public void Clear()
  {
    _dataCache.Clear();

    if (_index.Count == 0) return;

    lock (_indexFile)
    {
      _index.Clear();
      WriteIndex();
    }

    _logger.LogDebug("Cleared all persisted data");
  }

  private void WriteIndex()
  {
    lock (_indexFile)
    {
      WriteFile(_indexFile, JsonSerializer.Serialize(_index, _serializerOptions));
    }
  }

  private void Cleanup()
  {
    if (_index.Count == 0) return;

    // remove from index if file no longer exists
    foreach (var (key, value) in new Dictionary<string, string>(_index))
    {
      if (!File.Exists(DataFilePath(value))) _index.Remove(key);
    }

    // remove file if no longer in index 
    var valuesInIndex = new List<string>(_index.Values);
    foreach (var file in Directory.GetFiles(_storagePath))
    {
      if (Path.GetFileName(file) == IndexFileName) continue;
      if (valuesInIndex.Contains(Path.GetFileName(file))) continue;

      _logger.LogInformation("Removing file not present in index: {File}", file);
      File.Delete(file);
    }
  }

  private Dictionary<string, string> IndexFromFile()
  {
    if (!File.Exists(_indexFile))
    {
      return new Dictionary<string, string>();
    }

    try
    {
      var jsonData = File.ReadAllText(_indexFile);
      if (string.IsNullOrWhiteSpace(jsonData))
      {
        return new Dictionary<string, string>();
      }

      return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData, _serializerOptions) ??
             new Dictionary<string, string>();
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Failed to read persisted plugin data");
      throw new PluginException("Failed to read persisted plugin data", e);
    }
  }

  private StorageEntry CreateEntry(string key, object data)
  {
    try
    {
      return new StorageEntry(
        Key: key,
        Type: data.GetType().FullName ?? throw new PluginException("Failed to determine type of data to persist"),
        Ts: DateTimeOffset.UtcNow,
        Data: JsonSerializer.Serialize(data, _serializerOptions)
      );
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Failed to serialize data for key: {Key}", key);
      var pe = new PluginException("Failed to serialize data" + key, e);
      pe.Details.Add("key", key);
      throw pe;
    }
  }

  private string GenerateFileName()
  {
    for (var i = 0; i < 10; i++)
    {
      var fileName = $"{Path.GetRandomFileName()}.json";
      if (!File.Exists(DataFilePath(fileName))) return fileName;
    }

    throw new Exception("Failed to generate unique file name");
  }

  private string DataFilePath(string fileName) => Path.Join(_storagePath, fileName);

  private void WriteFile(string file, string content)
  {
    _logger.LogDebug("Writing file {File}", file);
    var tmpFile = file + ".tmp";
    if (File.Exists(tmpFile))
    {
      File.Delete(tmpFile);
    }

    File.WriteAllText(tmpFile, content);
    File.Move(tmpFile, file, true);
  }
}

internal sealed record StorageEntry(string Key, string Type, DateTimeOffset Ts, string Data);