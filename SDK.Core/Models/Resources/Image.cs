namespace MoBro.Plugin.SDK.Models.Resources;

/// <inheritdoc />
public sealed class Image : IImage
{
  /// <summary>
  /// Creates a new image
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="alt">The alternative text</param>
  /// <param name="relativeFilePath">The relative path to image file</param>
  public Image(string id, string? alt, string relativeFilePath)
  {
    Id = id;
    Alt = alt;
    RelativeFilePath = relativeFilePath;
  }

  /// <inheritdoc />
  public string Id { get; }

  /// <inheritdoc />
  public string? Alt { get; }

  /// <inheritdoc />
  public string RelativeFilePath { get; }
}