using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Allows the plugin to interact with MoBro (e.g. register items, update metric values,...).
/// </summary>
public interface IMoBroService
{
  /// <summary>
  /// Registers new items with the service.
  /// </summary>
  /// <param name="items">The items to register.</param>
  /// <exception cref="System.ArgumentNullException">The items are null.</exception>
  void Register(IEnumerable<IMoBroItem> items);

  /// <summary>
  /// Registers a new item with the service.
  /// </summary>
  /// <param name="item">The item to register.</param>
  /// <exception cref="System.ArgumentNullException">The item is null.</exception>
  void Register(IMoBroItem item);

  /// <summary>
  /// Unregister items with the service.
  /// </summary>
  /// <param name="ids">The ids of the items to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The ids are null.</exception>
  void Unregister(IEnumerable<string> ids);

  /// <summary>
  /// Unregister an item with the service.
  /// </summary>
  /// <param name="id">The id of the item to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The id is null.</exception>
  void Unregister(string id);

  /// <summary>
  /// Gets the registered item associated with the specified id.
  /// </summary>
  /// <param name="id">The id of the item to get</param>
  /// <param name="item">When this method returns, contains the registered item associated with the specified id; otherwise null.</param>
  /// <typeparam name="T">The type of the item</typeparam>
  /// <returns>true if an item with the given id and type is registered; otherwise false</returns>
  /// <exception cref="System.ArgumentNullException">The id is null.</exception>
  bool TryGet<T>(string id, [MaybeNullWhen(false)] out T item) where T : IMoBroItem;

  /// <summary>
  /// Clears (=unregisters) all currently registered items.
  /// </summary>
  void ClearRegistration();

  /// <summary>
  /// Push a new value for one or more registered <see cref="Metric"/>s
  /// </summary>
  /// <param name="values">The <see cref="MetricValue"/>s</param>
  void UpdateMetricValues(IEnumerable<MetricValue> values);

  /// <summary>
  /// Push a new value for one registered <see cref="Metric"/>
  /// </summary>
  /// <param name="value">The <see cref="MetricValue"/></param>
  void UpdateMetricValue(in MetricValue value);

  /// <summary>
  /// Push a new value for a registered <see cref="Metric"/>
  /// </summary>
  /// <param name="id">The id of the metric</param>
  /// <param name="value">The new value of the metric</param>
  /// <param name="timestamp">The date and time the value was recorded or measured at (in UTC)</param>
  void UpdateMetricValue(string id, object? value, DateTime timestamp);

  /// <summary>
  /// Push a new value for a registered <see cref="Metric"/> with the timestamp automatically set to <see cref="DateTime.UtcNow"/>
  /// </summary>
  /// <param name="id">The id of the metric</param>
  /// <param name="value">The new value of the metric</param>
  void UpdateMetricValue(string id, object? value);

  /// <summary>
  /// Gets the current value assigned to the metric with the specified id.
  /// </summary>
  /// <param name="id">The id of the metric to get the value for</param>
  /// <returns>The <see cref="MetricValue"/> currently assigned to the metric.</returns>
  /// <exception cref="System.ArgumentNullException">The id is null.</exception>
  MetricValue GetMetricValue(string id);

  /// <summary>
  /// Notifies the service that an unrecoverable error has occurred.
  /// This will cause the service to terminate the plugin.
  /// </summary>
  /// <param name="message">The error message</param>
  void Error(string message);

  /// <summary>
  /// Notifies the service that an unrecoverable error has occured.
  /// This will cause the service to terminate the plugin.
  /// </summary>
  /// <param name="exception">The occurred exception</param>
  void Error(Exception exception);
}