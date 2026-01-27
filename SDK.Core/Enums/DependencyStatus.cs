namespace MoBro.Plugin.SDK.Enums;

/// <summary>
/// Represents the status of a dependency within the system.
/// </summary>
public enum DependencyStatus
{
  /// <summary>
  /// Indicates that the status of the dependency is unknown.
  /// </summary>
  Unknown,

  /// <summary>
  /// Indicates that the dependency is in a healthy state.
  /// </summary>
  Ok,

  /// <summary>
  /// Indicates that the dependency is missing and cannot be used.
  /// </summary>
  Missing,

  /// <summary>
  /// Indicates that the dependency is outdated and should be updated.
  /// </summary>
  Outdated,
}