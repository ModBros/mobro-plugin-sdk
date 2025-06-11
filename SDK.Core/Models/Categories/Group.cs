using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Categories;

/// <summary>
/// Groups items of a common context (E.g. all metrics of the primary GPU) 
/// </summary>
/// <remarks>
/// A group may contain items from different categories
/// </remarks>
public sealed class Group : IMoBroItem, IMoBroLabeledItem
{
  /// <inheritdoc />
  [Required]
  [Length(1, 256)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public required string Id { get; set; }

  /// <summary>
  /// The visible category label
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string Label { get; set; }

  /// <summary>
  /// An optional further description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }


  /// <summary>
  /// An optional icon id
  /// </summary>
  [Length(1, 256)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? Icon { get; set; }

  /// <summary>
  /// Optional sub groups
  /// </summary>
  [MaxLength(10)]
  public IEnumerable<Group>? SubGroups { get; set; } = new List<Group>();
}