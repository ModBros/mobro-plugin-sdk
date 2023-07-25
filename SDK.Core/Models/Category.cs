using System;
using System.Collections.Generic;

namespace MoBro.Plugin.SDK.Models;

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
  public string Label { get; set; }

  /// <summary>
  /// An optional further description
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// An optional icon id
  /// </summary>
  public string? Icon { get; set; }

  /// <summary>
  /// Optional sub categories
  /// </summary>
  public IEnumerable<Category>? SubCategories { get; set; } = new List<Category>();
}