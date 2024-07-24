namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Provides and easy way for the plugin to serialize and persist arbitrary objects to disk.
/// Objects are persisted to the plugins data directory and remain accessible between plugin starts.
/// </summary>
public interface IMoBroPersistenceManager
{
  /// <summary>
  /// Stores data with the specified key.
  /// </summary>
  /// <typeparam name="T">The type of data to store.</typeparam>
  /// <param name="key">The unique key to identify the data.</param>
  /// <param name="data">The data to be stored.</param>
  void Put<T>(string key, T data);

  /// <summary>
  /// Retrieves data associated with the specified key.
  /// </summary>
  /// <typeparam name="T">The type of data to retrieve.</typeparam>
  /// <param name="key">The key to identify the data.</param>
  /// <returns>The stored data, or default(T) if the key is not found or the object is not of type T.</returns>
  T? Get<T>(string key);

  /// <summary>
  /// Removes data associated with the specified key.
  /// </summary>
  /// <param name="key">The key to identify the data to remove.</param>
  /// <returns>True if data was removed; otherwise, false.</returns> 
  bool Remove(string key);

  /// <summary>
  /// Checks if data associated with the specified key exists in the store.
  /// </summary>
  /// <param name="key">The key to check for existence.</param>
  /// <returns>True if data with the key exists; otherwise, false.</returns>
  bool Exists(string key);

  /// <summary>
  /// Deletes all data.
  /// </summary>
  void Clear();
}