using System;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// Represents a generic error that occurs during plugin execution.
/// </summary>
public class PluginException : Exception
{
  /// <inheritdoc />
  public PluginException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public PluginException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}