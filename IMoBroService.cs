using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK;

/// <summary>
/// MoBro object passed to the plugin upon initialization.
/// Allows the plugin to interact with the service (e.g. register new metrics).
/// </summary>
public interface IMoBroService
{
  /// <summary>
  /// Registers new items with the service.
  /// </summary>
  /// <param name="items">The items to register.</param>
  /// <exception cref="System.ArgumentNullException">The items are null.</exception>
  void RegisterItems(IEnumerable<IMoBroItem> items);

  /// <summary>
  /// Registers a new item with the service.
  /// </summary>
  /// <param name="item">The item to register.</param>
  /// <exception cref="System.ArgumentNullException">The item is null.</exception>
  void RegisterItem(IMoBroItem item);

  /// <summary>
  /// Unregister items with the service.
  /// </summary>
  /// <param name="ids">The ids of the items to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The ids are null.</exception>
  void UnregisterItems(IEnumerable<string> ids);

  /// <summary>
  /// Unregister a item with the service.
  /// </summary>
  /// <param name="id">The id of the item to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The id is null.</exception>
  void UnregisterItem(string id);

  /// <summary>
  /// Gets the registered item associated with the specified id.
  /// </summary>
  /// <param name="id">The id of the item to get</param>
  /// <param name="item">When this method returns, contains the registered item associated with the specified id; otherwise, null.</param>
  /// <typeparam name="T">The type of the item</typeparam>
  /// <returns>true if an item with the given id and type is registered; otherwise false</returns>
  /// <exception cref="System.ArgumentNullException">The id is null.</exception>
  bool TryGetItem<T>(string id, [MaybeNullWhen(false)] out T item) where T : IMoBroItem;

  /// <summary>
  /// Clears all currently registered items.
  /// </summary>
  void ClearItemRegister();

  /// <summary>
  /// Push a new value for one or more registered <see cref="IMetric"/>s
  /// </summary>
  /// <param name="values">The <see cref="MetricValue"/>s</param>
  void UpdateMetricValues(IEnumerable<MetricValue> values);

  /// <summary>
  /// Push a new value for one registered <see cref="IMetric"/>
  /// </summary>
  /// <param name="value">The <see cref="MetricValue"/></param>
  void UpdateMetricValue(in MetricValue value);

  /// <summary>
  /// Push a new value for a registered <see cref="IMetric"/>
  /// </summary>
  /// <param name="id">The id of the metric</param>
  /// <param name="value">The new value of the metric</param>
  /// <param name="timestamp">The date and time the value was recorded or measured at</param>
  void UpdateMetricValue(string id, object? value, DateTime timestamp);

  /// <summary>
  /// Push a new value for a registered <see cref="IMetric"/> with the timestamp automatically set to <see cref="DateTime.UtcNow"/>
  /// </summary>
  /// <param name="id">The id of the metric</param>
  /// <param name="value">The new value of the metric</param>
  void UpdateMetricValue(string id, object? value);
}