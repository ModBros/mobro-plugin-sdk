using System;
using System.Collections.Generic;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// Represents a generic error that occurs during plugin execution.
/// </summary>
public class PluginException : Exception
{
  /// <summary>
  /// Additional exception details.
  /// Content depends on the specific exception.
  /// </summary>
  public IDictionary<string, string> Details { get; } = new Dictionary<string, string>();

  /// <inheritdoc />
  public PluginException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public PluginException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  /// <summary>
  /// Add an additional detail to the exception
  /// </summary>
  /// <param name="key">The key</param>
  /// <param name="detail">The detail value</param>
  /// <returns>The exception</returns>
  public PluginException AddDetail(string? key, string? detail)
  {
    if (key == null || detail == null) return this;
    Details[key] = detail;
    return this;
  }
}