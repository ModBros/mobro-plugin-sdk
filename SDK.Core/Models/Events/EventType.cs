namespace MoBro.Plugin.SDK.Models.Events;

/// <summary>
/// Defines the severity levels for events.
/// </summary>
public enum EventType
{
  /// <summary>
  /// General informational message 
  /// </summary>
  Information,

  /// <summary>
  /// Warnings that should be noted but do not require immediate action 
  /// </summary>
  Warning,

  /// <summary>
  /// An occurred error
  /// </summary>
  Error,

  /// <summary>
  /// Critical issue
  /// </summary>
  Critical,
}