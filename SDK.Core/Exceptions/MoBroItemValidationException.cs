using System;
using MoBro.Plugin.SDK.Models;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// An error indicating that a passed <see cref="IMoBroItem"/> was invalid.
/// </summary>
public sealed class MoBroItemValidationException : PluginException
{
  /// <inheritdoc />
  public MoBroItemValidationException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public MoBroItemValidationException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  /// <summary>
  /// Creates a new MoBroItemValidationException
  /// </summary>
  /// <param name="mobroItemId">The id of the <see cref="IMoBroItem"/></param>
  /// <param name="message">The message</param>
  /// <param name="innerException">The inner exception</param>
  public MoBroItemValidationException(string mobroItemId, string message, Exception? innerException = null) : base(
    message,
    innerException)
  {
    AddDetail("itemId", mobroItemId);
  }
}