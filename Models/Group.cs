using System;
using System.Collections.Generic;
using System.Linq;

namespace MoBro.Plugin.SDK.Models;

/// <inheritdoc />
public sealed class Group : IGroup
{
  /// <summary>
  /// Creates a new group with the given properties
  /// </summary>
  /// <param name="id">The group id</param>
  /// <param name="label">The group label</param>
  /// <param name="description">The optional description</param>
  /// <param name="icon">The optional icon id</param>
  public Group(string id, string label, string? description = null, string? icon = null)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
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
  public IEnumerable<IGroup>? SubGroups { get; set; } = Enumerable.Empty<IGroup>();
}