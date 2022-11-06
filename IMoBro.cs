using System.Diagnostics.CodeAnalysis;
using MoBro.Plugin.SDK.Models;

namespace MoBro.Plugin.SDK;

/// <summary>
/// MoBro object passed to the plugin upon initialization.
/// Allows the plugin to interact with the service (e.g. register new metrics).
/// </summary>
public interface IMoBro
{
  /// <summary>
  /// Registers new items with the service.
  /// </summary>
  /// <param name="items">The items to register.</param>
  /// <exception cref="System.ArgumentNullException">The items are null.</exception>
  void Register(params IMoBroItem[] items);

  /// <summary>
  /// Unregister items with the service.
  /// </summary>
  /// <param name="ids">The ids of the items to unregister.</param>
  /// <exception cref="System.ArgumentNullException">The ids are null.</exception>
  void Unregister(params string[] ids);

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
  void Clear();
}