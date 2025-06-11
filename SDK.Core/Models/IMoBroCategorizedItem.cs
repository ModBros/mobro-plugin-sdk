namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// Represents an item that can be assigned to a category and an optional group.
/// </summary>
public interface IMoBroCategorizedItem
{
  /// <summary>
  /// The ID of the category this item is assigned to.
  /// </summary>
  public string CategoryId { get; set; }

  /// <summary>
  /// The optional ID of the group this item is part of.
  /// </summary>
  public string? GroupId { get; set; }
}