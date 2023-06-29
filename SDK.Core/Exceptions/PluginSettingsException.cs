using System;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// An error indicating that the plugin is missing a required setting or the provided setting value is invalid.
/// </summary>
public sealed class PluginSettingsException : PluginException
{
  /// <inheritdoc />
  public PluginSettingsException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public PluginSettingsException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  /// <summary>
  /// Creates a new PluginSettingsException
  /// </summary>
  /// <param name="field">The key of the setting field causing the error</param>
  /// <param name="message">The message</param>
  /// <param name="innerException">The inner exception</param>
  public PluginSettingsException(string? field, string? message, Exception? innerException) : base(message,
    innerException)
  {
    if (field != null)
    {
      AddDetail("field", field);
    }
  }
}