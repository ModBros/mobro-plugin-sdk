using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;

namespace MoBro.Plugin.SDK.Services;

internal sealed class MoBroPersistenceManager : IMoBroPersistenceManager
{
  private const string IndexFileName = "index.json";

  private readonly ILogger _logger;
  private readonly string _indexFile;
  private readonly string _storagePath;

  private readonly Dictionary<string, string> _index;
  private readonly Dictionary<string, object?> _dataCache;

  public MoBroPersistenceManager(string storagePath, ILogger logger)
  {
    Directory.CreateDirectory(storagePath);
    _logger = logger;
    _storagePath = storagePath;
    _indexFile = Path.Combine(storagePath, IndexFileName);
    _index = IndexFromFile();
    _dataCache = new Dictionary<string, object?>();
    Cleanup();
  }

  public void Put<T>(string key, T data) where T : class
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
      File.WriteAllText(DataFilePath(fileName), JsonSerializer.Serialize(entry));
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

  public T? Get<T>(string key) where T : class
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
      var entry = JsonSerializer.Deserialize<StorageEntry>(File.ReadAllText(DataFilePath(fileName)));
      if (entry == null) return default;
      if (entry.Type.Equals(typeof(T).FullName))
      {
        var data = JsonSerializer.Deserialize<T>(entry.Data);
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

  public bool Exists(string key) => _index.ContainsKey(key);

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
      File.WriteAllText(_indexFile, JsonSerializer.Serialize(_index));
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

      return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData) ?? new Dictionary<string, string>();
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
        Data: JsonSerializer.Serialize(data)
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
      var fileName = $"{Guid.NewGuid()}.json";
      if (!File.Exists(DataFilePath(fileName))) return fileName;
    }

    throw new Exception("Failed to generate unique file name");
  }

  private string DataFilePath(string fileName) => Path.Join(_storagePath, fileName);
}

internal sealed record StorageEntry(string Key, string Type, DateTimeOffset Ts, string Data);