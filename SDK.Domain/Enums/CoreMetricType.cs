namespace MoBro.Plugin.SDK.Enums;

/// <summary>
/// Global metric types provided by the service for the most common types of values.
/// These are already well defined with their respective base and convertable units. Metric values must be provided in
/// the base unit as outlined in this file.
/// </summary>
public enum CoreMetricType
{
  /// <summary>
  /// Electric current (Ampere)
  /// </summary>
  ElectricCurrent,

  /// <summary>
  /// Electric potential difference (Volt)
  /// </summary>
  ElectricPotential,

  /// <summary>
  /// Electric resistance (Ohm)
  /// </summary>
  ElectricResistance,

  /// <summary>
  /// Bitrate (Bit per second)
  /// </summary>
  DataFlow,

  /// <summary>
  /// Capacity of data (Bytes)
  /// </summary>
  Data,

  /// <summary>
  /// The amount of time elapsed between two events (ISO 8601 duration string or C# TimeSpan object)
  /// </summary>
  Duration,

  /// <summary>
  /// Number of occurrences of a repeating event per unit of time (Hertz)
  /// </summary>
  Frequency,

  /// <summary>
  /// A multiplier
  /// </summary>
  Multiplier,

  /// <summary>
  /// A simple numeric value
  /// </summary>
  Numeric,

  /// <summary>
  /// Electric power (Watt)
  /// </summary>
  Power,

  /// <summary>
  /// Rotational Speed (Rotations per minute)
  /// </summary>
  Rotation,

  /// <summary>
  /// Temperature (in degree Celsius)
  /// </summary>
  Temperature,

  /// <summary>
  /// A simple text value
  /// </summary>
  Text,

  /// <summary>
  /// Utilization indication in percentage
  /// </summary>
  Usage,

  /// <summary>
  /// Applied force on a surface (Pascal)
  /// </summary>
  Pressure,

  /// <summary>
  /// A measure of occupied space in 3D (Cubic metre)
  /// </summary>
  Volume,

  /// <summary>
  /// Volume which passes per unit of time (Cubic metre per second)
  /// </summary>
  VolumeFlow,

  /// <summary>
  /// A date and time (ISO 8601 date + time string or C# DateTime/DateTimeOffset object)
  /// </summary>
  DateTime,

  /// <summary>
  /// A measure of distance (Metres)
  /// </summary>
  Length,

  /// <summary>
  /// Distance travelled per unit of time (Metres per second)
  /// </summary>
  Speed,

  /// <summary>
  /// The quantity of matter of a physical body (Kilogram)
  /// </summary>
  Mass,

  /// <summary>
  /// Quantity to express the extent of a region (Square meter)
  /// </summary>
  Area,

  /// <summary>
  /// A date without the time component (ISO 8601 date string or C# DateOnly object)
  /// </summary>
  Date,

  /// <summary>
  /// A time without the date component (ISO 8601 time string or C# TimeOnly object)
  /// </summary>
  Time,

  /// <summary>
  /// An icon (id of a registered resource)
  /// </summary>
  Icon,

  /// <summary>
  /// An image (id of a registered resource)
  /// </summary>
  Image,

  /// <summary>
  /// An angle (degrees) 
  /// </summary> 
  Angle,
}