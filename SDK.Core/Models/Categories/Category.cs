using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Categories;

/// <summary>
/// Categorizes items of the same kind (E.g. all CPU related metrics)
/// </summary>
public sealed class Category : IMoBroItem
{
  /// <summary>
  /// Creates a new category with the given properties
  /// </summary>
  /// <param name="id">The category id</param>
  /// <param name="label">The category label</param>
  /// <param name="description">The optional description</param>
  /// <param name="icon">The optional icon id</param>
  public Category(string id, string label, string? description = null, string? icon = null)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
    Description = description;
    Icon = icon;
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <summary>
  /// The visible category label
  /// </summary>
  [Required]
  [Length(1, 32)]
  public string Label { get; set; }

  /// <summary>
  /// An optional further description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }

  /// <summary>
  /// An optional icon id
  /// </summary>
  [MaxLength(256)]
  public string? Icon { get; set; }

  /// <summary>
  /// Optional sub categories
  /// </summary>
  [MaxLength(10)]
  public IEnumerable<Category>? SubCategories { get; set; } = new List<Category>();
}