using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Extensions;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Implementation of <see cref="IMoBroService"/> for local testing.
/// </summary>
public sealed class MoBroService : IMoBroService
{
  private readonly ILogger _logger;
  private readonly IDictionary<string, IMoBroItem> _items;
  private readonly IDictionary<string, MetricValue> _metricValues;

  private const int IdShortenLength = 36;

  /// <summary>
  /// Instantiates a new MoBroService
  /// </summary>
  /// <param name="logger">An instance of <see cref="ILogger"/></param>
  public MoBroService(ILogger logger)
  {
    _logger = logger;
    _items = new ConcurrentDictionary<string, IMoBroItem>();
    _metricValues = new ConcurrentDictionary<string, MetricValue>();
  }

  /// <inheritdoc />
  public void Register(IEnumerable<IMoBroItem> items)
  {
    foreach (var item in Guard.Against.Null(items))
    {
      Register(item);
    }
  }

  /// <inheritdoc />
  public void Register(IMoBroItem item)
  {
    _items[item.Id] = Guard.Against.Null(item).Validate(this);
    _logger.LogInformation("Registered {ItemType}: {ItemId}", item.GetType().Name, item.Id);
    if (item.Id.Length > IdShortenLength)
    {
      _logger.LogInformation(
        "ID '{ItemId}' exceeds length of {ItemIdMaxLength} and will be mapped to a shorter ID for internal use in MoBro",
        item.Id, IdShortenLength
      );
    }
  }

  /// <inheritdoc />
  public void Unregister(IEnumerable<string> ids)
  {
    foreach (var id in Guard.Against.Null(ids))
    {
      Unregister(id);
    }
  }

  /// <inheritdoc />
  public void Unregister(string id)
  {
    _items.Remove(Guard.Against.NullOrEmpty(id));
    _metricValues.Remove(id);
    _logger.LogInformation("Unregistered Item: {ItemId}", id);
  }

  /// <inheritdoc />
  public IEnumerable<IMoBroItem> GetAll()
  {
    return _items.Values;
  }

  /// <inheritdoc />
  public IEnumerable<T> GetAll<T>() where T : IMoBroItem
  {
    return _items.Values
      .Where(i => i.GetType().IsAssignableTo(typeof(T)))
      .Cast<T>();
  }

  /// <inheritdoc />
  public bool TryGet<T>(string id, [MaybeNullWhen(false)] out T item) where T : IMoBroItem
  {
    if (_items.TryGetValue(Guard.Against.NullOrEmpty(id), out var knownItem) &&
        knownItem.GetType().IsAssignableTo(typeof(T)))
    {
      item = (T)knownItem;
      return true;
    }

    item = default;
    return false;
  }

  /// <inheritdoc />
  public void ClearRegistration()
  {
    _items.Clear();
    _logger.LogInformation("Cleared all registrations");
  }

  /// <inheritdoc />
  public void UpdateMetricValues(IEnumerable<MetricValue> values)
  {
    foreach (var v in Guard.Against.Null(values))
    {
      UpdateMetricValue(v);
    }
  }

  /// <inheritdoc />
  public void UpdateMetricValue(in MetricValue value)
  {
    Guard.Against.Null(value).Validate(this);
    _metricValues[value.Id] = value;
    _logger.LogDebug("Value of metric {MetricId} updated to: {MetricValue}", value.Id, value.Value);
  }

  /// <inheritdoc />
  public void UpdateMetricValue(string id, object? value, DateTime timestamp)
  {
    UpdateMetricValue(new MetricValue(Guard.Against.NullOrEmpty(id), Guard.Against.Null(timestamp), value));
  }

  /// <inheritdoc />
  public void UpdateMetricValue(string id, object? value)
  {
    UpdateMetricValue(new MetricValue(Guard.Against.NullOrEmpty(id), value));
  }

  /// <inheritdoc />
  public IEnumerable<MetricValue> GetMetricValues()
  {
    return _metricValues.Values;
  }

  /// <inheritdoc />
  public MetricValue? GetMetricValue(string id)
  {
    return _metricValues.TryGetValue(Guard.Against.NullOrEmpty(id), out var knownValue) ? knownValue : null;
  }

  /// <inheritdoc />
  public void Error(string message)
  {
    Guard.Against.NullOrWhiteSpace(message);
    _logger.LogError("Plugin error: {PluginError}", message);
    throw new PluginException("An unexpected plugin error occurred: " + message);
  }

  /// <inheritdoc />
  public void Error(Exception exception)
  {
    Guard.Against.Null(exception);
    _logger.LogError(exception, "Plugin error: {PluginError}", exception.Message);
    throw new PluginException("An unexpected plugin error occurred", exception);
  }
}