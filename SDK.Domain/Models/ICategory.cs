using System.Collections.Generic;

namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// Categorizes items of the same kind (E.g. all CPU related metrics)
/// </summary>
public interface ICategory : IMoBroItem
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
  /// An optional icon id
  /// </summary>
  public string? Icon { get; }

  /// <summary>
  /// Optional sub categories
  /// </summary>
  public IEnumerable<ICategory>? SubCategories { get; }
}