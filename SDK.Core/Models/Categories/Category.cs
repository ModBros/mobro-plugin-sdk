using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Categories;

/// <summary>
/// Categorizes items of the same kind (E.g. all CPU related metrics)
/// </summary>
public sealed class Category : IMoBroItem
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
  /// Optional sub categories
  /// </summary>
  [MaxLength(10)]
  public IEnumerable<Category>? SubCategories { get; set; } = new List<Category>();
}