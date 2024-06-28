using System;

namespace MoBro.Plugin.SDK.Exceptions;

/// <summary>
/// An error indicating that an invalid value was passed for a metric.
/// </summary>
public sealed class MetricValueValidationException : PluginException
{
  /// <inheritdoc />
  public MetricValueValidationException(string? message) : base(message)
  {
  }

  /// <inheritdoc />
  public MetricValueValidationException(string? message, Exception? innerException) : base(message, innerException)
  {
  }

  /// <summary>
  /// Creates a new MetricValueValidationException
  /// </summary>
  /// <param name="metricId">The id of the metric</param>
  /// <param name="message">The message</param>
  /// <param name="innerException">The inner exception</param>
  public MetricValueValidationException(string metricId, string message, Exception? innerException = null) : base(
    message,
    innerException)
  {
    AddDetail("metricId", metricId);
  }
}