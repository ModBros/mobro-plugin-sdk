﻿using System.Collections.Generic;
using System.Linq;

namespace MoBro.Plugin.SDK.Models;

/// <inheritdoc />
public sealed class Category : ICategory
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
    Id = id;
    Label = label;
    Description = description;
    Icon = icon;
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <inheritdoc />
  public string Label { get; set; }

  /// <inheritdoc />
  public string? Description { get; set; }

  /// <inheritdoc />
  public string? Icon { get; set; }

  /// <inheritdoc />
  public IEnumerable<ICategory>? SubCategories { get; set; } = Enumerable.Empty<ICategory>();
}