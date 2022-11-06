using System;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// An error indicating that an error occurred in an external dependency (e.g. another program, web API,...) 
/// </summary>
public sealed class PluginDependencyException : PluginException
{
  /// <inheritdoc />
  public PluginDependencyException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public PluginDependencyException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}