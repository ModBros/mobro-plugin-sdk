﻿using MoBro.Plugin.SDK.Exceptions;

namespace MoBro.Plugin.SDK;

/// <summary>
/// The plugin specific settings passed the plugin upon initialization.
/// </summary>
public interface IPluginSettings
{
  /// <summary>
  /// Gets the current setting value for a given key.
  /// </summary>
  /// <param name="key">The key of the setting to get.</param>
  /// <typeparam name="T">The type of the return value (e.g. bool).</typeparam>
  /// <returns>The current value of the setting.</returns>
  /// <exception cref="System.ArgumentNullException">The key is null.</exception>
  /// <exception cref="PluginSettingsException">The setting does not exists or is not set.</exception>
  /// <exception cref="PluginException">An error occured while parsing the settings value.</exception>
  /// 
  T GetValue<T>(string key);

  /// <summary>
  /// Gets the current setting value for a given key. If no value is set, the given default value is returned instead.
  /// </summary>
  /// <param name="key">The key of the setting to get.</param>
  /// <param name="defaultValue">The default value to return in case the setting isn't set.</param>
  /// <typeparam name="T">The type of the return value (e.g. bool).</typeparam>
  /// <returns>The curren value of the setting if set; the default value otherwise.</returns>
  /// <exception cref="System.ArgumentNullException">The key is null.</exception>
  /// <exception cref="PluginException">An error occured while parsing the settings value.</exception>
  T GetValue<T>(string key, T defaultValue);

  /// <summary>
  /// Gets the current setting value for a given key.
  /// </summary>
  /// <param name="key">The key of the setting to get.</param>
  /// <param name="value">
  /// When this method returns, the value associated with the specified key, if the key is found;
  /// otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized
  /// </param>
  /// <typeparam name="T">The type of the return value (e.g. bool).</typeparam>
  /// <returns>true if the setting with the specified key is found; otherwise, false.</returns>
  /// <exception cref="System.ArgumentNullException">The key is null.</exception>
  /// <exception cref="PluginException">An error occured while parsing the settings value.</exception>
  bool TryGetValue<T>(string key, out T? value);
}