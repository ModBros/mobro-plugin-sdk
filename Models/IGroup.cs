using System.Collections.Generic;

namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// Groups items of a common context (E.g. all metrics of the primary GPU) 
/// </summary>
/// <remarks>
/// A group may contain items from different categories
/// </remarks>
public interface IGroup : IMoBroItem
{
  /// <summary>
  /// The visible category label
  /// </summary>
  public string Label { get; }

  /// <summary>
  /// An optional further description
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// An optional icon (relative path to the icon file)
  /// </summary>
  public string? Icon { get; }

  /// <summary>
  /// Optional sub groups
  /// </summary>
  public IEnumerable<IGroup>? SubGroups { get; }
}