using System;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Events;

/// <summary>
/// Represents an event with a specific type, timestamp, title, details, and optionally a metric identifier.
/// Events are published to an <see cref="EventStream"/>
/// </summary>
public readonly record struct Event
{
  /// <summary>
  /// The id of the <see cref="EventStream"/> this event belongs to.
  /// </summary>
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public required string EventStreamId { get; init; }

  /// <summary>
  /// The type of this event.
  /// </summary>
  [Required]
  public required EventType Type { get; init; }

  /// <summary>
  /// The timestamp indicating when the event occurred.
  /// </summary>
  [Required]
  public required DateTime Timestamp { get; init; }

  /// <summary>
  /// The title of this event.
  /// </summary>
  [Required]
  [Length(1, 128)]
  public required string Title { get; init; }

  /// <summary>
  /// Detailed information about the event.
  /// </summary>
  [MaxLength(255)]
  public string? Details { get; init; }

  /// <summary>
  /// The id oa a specific metric associated with the event.
  /// </summary>
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? MetricId { get; init; }
}