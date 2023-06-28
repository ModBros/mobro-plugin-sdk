namespace MoBro.Plugin.SDK.Enums;

/// <summary>
/// Types of supported metric values
/// </summary>
public enum MetricValueType
{
  /// <summary>
  /// A custom value (custom poco,...)
  /// </summary>
  Custom,

  /// <summary>
  /// A simple string value
  /// </summary>
  String,

  /// <summary>
  /// Any numeric value
  /// </summary>
  Numeric,

  /// <summary>
  /// An ISO 8601 duration string or C# TimeSpan object
  /// </summary>
  Duration,

  /// <summary>
  /// An ISO 8601 date + time string or C# DateTime/DateTimeOffset object
  /// </summary>
  DateTime,

  /// <summary>
  /// An ISO 8601 date string or C# DateOnly object
  /// </summary>
  DateOnly,

  /// <summary>
  /// An ISO 8601 time string or C# TimeOnly object
  /// </summary>
  TimeOnly,

  /// <summary>
  /// The id of a registered external resource (icon, image..)
  /// </summary>
  Resource
}