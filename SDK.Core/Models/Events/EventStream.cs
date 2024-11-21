using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Events;

/// <summary>
/// Represents an event stream that can be registered to MoBro.
/// </summary>
public sealed class EventStream : IMoBroItem
{
  /// <inheritdoc />
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public required string Id { get; set; }

  /// <summary>
  /// The textual name of the metric stream
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string Label { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }
}