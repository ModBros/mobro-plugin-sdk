namespace MoBro.Plugin.SDK.Models.Resources;

/// <summary>
/// A file based item (icon, image, ...)
/// </summary>
public interface IResource : IMoBroItem
{
  /// <summary>
  /// An optional alternative text to show if the resource fails to display
  /// </summary>
  public string? Alt { get; set; }
}