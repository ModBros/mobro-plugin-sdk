using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Timers;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;

namespace MoBro.Plugin.SDK.Services;

internal sealed class MoBroPersistence : IMoBroPersistence
{
  private const string DbFileName = "data.json";
  private static readonly TimeSpan WriteDelay = TimeSpan.FromMilliseconds(100);

  private readonly ILogger _logger;
  private readonly string _dbFile;
  private readonly IDictionary<string, StorageEntry> _dictionary;
  private readonly Timer _timer;

  public MoBroPersistence(string storageDirectory, ILogger logger)
  {
    Directory.CreateDirectory(storageDirectory);
    _logger = logger;
    _dbFile = Path.Combine(storageDirectory, DbFileName);
    _dictionary = ReadData();

    _timer = new Timer
    {
      AutoReset = false,
      Enabled = false,
      Interval = WriteDelay.TotalMilliseconds
    };
    _timer.Elapsed += OnTimer;
  }

  public void Put<T>(string key, T data)
  {
    Guard.Against.Null(data);
    Guard.Against.NullOrEmpty(key);

    lock (this)
    {
      _dictionary[key] = new StorageEntry(
        Type: data.GetType().FullName,
        Ts: DateTimeOffset.UtcNow,
        Data: JsonSerializer.Serialize(data)
      );
      if (!_timer.Enabled) _timer.Start();
    }
  }

  public T? Get<T>(string key)
  {
    if (!_dictionary.TryGetValue(key, out var value))
    {
      return default;
    }

    try
    {
      return JsonSerializer.Deserialize<T>(value.Data);
    }
    catch (JsonException e)
    {
      _logger.LogError(e, "Failed to deserialize persisted data for key \'{Key}\'", key);
      return default;
    }
  }

  public bool Remove(string key)
  {
    lock (this)
    {
      var removed = _dictionary.Remove(key);
      if (removed && !_timer.Enabled) _timer.Start();
      return removed;
    }
  }

  public bool Exists(string key) => _dictionary.ContainsKey(key);

  public void Clear()
  {
    lock (this)
    {
      _dictionary.Clear();
      if (!_timer.Enabled) _timer.Start();
    }
  }

  private void OnTimer(object? source, ElapsedEventArgs e)
  {
    lock (this)
    {
      // persist data
      File.WriteAllText(_dbFile, JsonSerializer.Serialize(_dictionary));
    }
  }

  private IDictionary<string, StorageEntry> ReadData()
  {
    if (!File.Exists(_dbFile))
    {
      return new Dictionary<string, StorageEntry>();
    }

    var jsonData = File.ReadAllText(_dbFile);
    if (string.IsNullOrWhiteSpace(jsonData))
    {
      return new Dictionary<string, StorageEntry>();
    }

    return JsonSerializer.Deserialize<IDictionary<string, StorageEntry>>(jsonData) ??
           throw new PluginException("Failed to read persisted data");
  }
}

internal sealed record StorageEntry(string? Type, DateTimeOffset Ts, string Data);