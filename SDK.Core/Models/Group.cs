using System;
using System.Collections.Generic;

namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// Groups items of a common context (E.g. all metrics of the primary GPU) 
/// </summary>
/// <remarks>
/// A group may contain items from different categories
/// </remarks>
public sealed class Group : IMoBroItem 
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

  /// <summary>
  /// The visible category label
  /// </summary>
  public string Label { get; set; }

  /// <summary>
  /// An optional further description
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// An optional icon (relative path to the icon file)
  /// </summary>
  public string? Icon { get; set; }

  /// <summary>
  /// Optional sub groups
  /// </summary>
  public IEnumerable<Group>? SubGroups { get; set; } = new List<Group>();
}