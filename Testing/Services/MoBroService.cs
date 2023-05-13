using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Testing.Services;

internal sealed class MoBroService : IMoBroService
{
  private readonly ILogger _logger;
  private readonly IDictionary<string, IMoBroItem> _items;

  public MoBroService(ILogger logger)
  {
    _logger = logger;
    _items = new Dictionary<string, IMoBroItem>();
  }

  public void Register(IEnumerable<IMoBroItem> items)
  {
    foreach (var item in Guard.Against.Null(items))
    {
      _items[item.Id] = item;
      _logger.LogDebug("Registered Item: {ItemId}", item.Id);
    }
  }

  public void Register(IMoBroItem item)
  {
    _items[item.Id] = Guard.Against.Null(item);
    _logger.LogDebug("Registered Item: {ItemId}", item.Id);
  }

  public void Unregister(IEnumerable<string> ids)
  {
    foreach (var id in Guard.Against.Null(ids))
    {
      _items.Remove(id);
      _logger.LogDebug("Unregistered Item: {ItemId}", id);
    }
  }

  public void Unregister(string id)
  {
    _items.Remove(Guard.Against.NullOrEmpty(id));
    _logger.LogDebug("Unregistered Item: {ItemId}", id);
  }

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

  public void ClearRegistration()
  {
    _items.Clear();
    _logger.LogDebug("Cleared all registrations");
  }

  public void UpdateMetricValues(IEnumerable<MetricValue> values)
  {
    foreach (var v in Guard.Against.Null(values))
    {
      _logger.LogDebug("Value of metric {MetricId} updated to: {MetricValue}", v.Id, v.Value);
    }
  }

  public void UpdateMetricValue(in MetricValue value)
  {
    Guard.Against.Null(value);
    _logger.LogDebug("Value of metric {MetricId} updated to: {MetricValue}", value.Id, value.Value);
  }

  public void UpdateMetricValue(string id, object? value, DateTime timestamp)
  {
    Guard.Against.NullOrEmpty(id);
    Guard.Against.Null(timestamp);
    _logger.LogDebug("Value of metric {MetricId} updated to: {MetricValue}", id, value);
  }

  public void UpdateMetricValue(string id, object? value)
  {
    Guard.Against.NullOrEmpty(id);
    _logger.LogDebug("Value of metric {MetricId} updated to: {MetricValue}", id, value);
  }

  public void Error(string message)
  {
    Guard.Against.NullOrEmpty(message);
    _logger.LogError("Plugin error: {PluginError}", message);
    throw new PluginException("An unexpected plugin error occurred: " + message);
  }

  public void Error(Exception exception)
  {
    Guard.Against.Null(exception);
    _logger.LogError(exception, "Plugin error: {PluginError}", exception.Message);
    throw new PluginException("An unexpected plugin error occurred", exception);
  }

  public IEnumerable<T> GetItems<T>() where T : IMoBroItem
  {
    return _items.Values
      .Where(i => i.GetType().IsAssignableTo(typeof(T)))
      .Cast<T>();
  }

  public IEnumerable<IMoBroItem> GetItems()
  {
    return _items.Values;
  }
}