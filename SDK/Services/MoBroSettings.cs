using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using Ardalis.GuardClauses;
using MoBro.Plugin.SDK.Exceptions;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Implementation of <see cref="IMoBroSettings"/> for local testing.
/// </summary>
public sealed class MoBroSettings : IMoBroSettings
{
  private readonly IDictionary<string, string> _settings;

  /// <summary>
  /// Instantiates MoBroSettings
  /// </summary>
  /// <param name="settings">The settings</param>
  public MoBroSettings(IDictionary<string, string>? settings = null)
  {
    _settings = settings ?? ImmutableDictionary<string, string>.Empty;
  }

  /// <inheritdoc />
  public T GetValue<T>(string key)
  {
    Guard.Against.NullOrEmpty(key);
    if (_settings.TryGetValue(key, out var val)) return ParseValue<T>(key, val);
    throw new PluginSettingsException(key, $"Missing setting: {key}", null);
  }

  /// <inheritdoc />
  public T GetValue<T>(string key, T defaultValue)
  {
    Guard.Against.NullOrEmpty(key);
    return _settings.TryGetValue(key, out var val) ? ParseValue<T>(key, val) : defaultValue;
  }

  /// <inheritdoc />
  public bool TryGetValue<T>(string key, out T? value)
  {
    Guard.Against.NullOrEmpty(key);
    if (!_settings.TryGetValue(key, out var val))
    {
      value = default;
      return false;
    }

    value = ParseValue<T>(key, val);
    return value != null;
  }

  private static T ParseValue<T>(string key, string value)
  {
    var t = typeof(T);
    if (t == typeof(string)) return (T)Convert.ChangeType(value, t);
    if (t == typeof(int)) return (T)Convert.ChangeType(int.Parse(value), t);
    if (t == typeof(bool)) return (T)Convert.ChangeType(bool.Parse(value), t);
    if (t == typeof(double))
      return (T)Convert.ChangeType(double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture), t);

    throw new PluginSettingsException(key, $"Unsupported settings type '{t}'", null);
  }
}