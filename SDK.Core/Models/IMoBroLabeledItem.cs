namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// Represents an item that has a label and an optional description for display purposes.
/// </summary>
public interface IMoBroLabeledItem
{
  /// <summary>
  /// The textual name of the item.
  /// </summary>
  public string Label { get; set; }

  /// <summary>
  /// An optional textual description of the item.
  /// </summary>
  public string? Description { get; set; }
}