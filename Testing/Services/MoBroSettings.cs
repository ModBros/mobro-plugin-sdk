using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using Ardalis.GuardClauses;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Testing.Services;

internal sealed class MoBroSettings : IMoBroSettings
{
  private readonly IDictionary<string, string> _settings;

  public MoBroSettings(IDictionary<string, string>? settings = null)
  {
    _settings = settings ?? ImmutableDictionary<string, string>.Empty;
  }

  public T GetValue<T>(string key)
  {
    Guard.Against.NullOrEmpty(key);
    if (_settings.TryGetValue(key, out var val)) return ParseValue<T>(val);
    throw new PluginSettingsException(key, $"Missing setting: {key}", null);
  }

  public T GetValue<T>(string key, T defaultValue)
  {
    Guard.Against.NullOrEmpty(key);
    return _settings.TryGetValue(key, out var val) ? ParseValue<T>(val) : defaultValue;
  }

  public bool TryGetValue<T>(string key, out T? value)
  {
    Guard.Against.NullOrEmpty(key);
    if (!_settings.TryGetValue(key, out var val))
    {
      value = default;
      return false;
    }

    value = ParseValue<T>(val);
    return value != null;
  }

  private static T ParseValue<T>(string value)
  {
    var t = typeof(T);
    if (t == typeof(string)) return (T)Convert.ChangeType(value, t);
    if (t == typeof(int)) return (T)Convert.ChangeType(int.Parse(value), t);
    if (t == typeof(double))
      return (T)Convert.ChangeType(double.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture), t);
    if (t == typeof(bool)) return (T)Convert.ChangeType(bool.Parse(value), t);

    throw new PluginException($"Unsupported settings type '{t}'");
  }
}