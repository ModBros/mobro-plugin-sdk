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
  void RegisterItem(params IMoBroItem[] items);

  /// <summary>
  /// Unregister items with the service.
  /// </summary>
  /// <param name="ids">The ids of the items to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The ids are null.</exception>
  void UnregisterItem(params string[] ids);

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
  void UpdateMetricValue(params MetricValue[] values);

}