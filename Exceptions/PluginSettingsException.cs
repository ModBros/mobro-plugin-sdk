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
}