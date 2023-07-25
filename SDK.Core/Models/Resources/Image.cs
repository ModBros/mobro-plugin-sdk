namespace MoBro.Plugin.SDK.Models.Resources;

/// <summary>
/// A larger image or picture.
/// In contrast to <see cref="Icon"/>, an image is only one single file and hence does not support different sizes.
/// </summary>
public sealed class Image : IResource 
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

  /// <summary>
  /// The relative path to the image file.
  /// </summary>
  public string RelativeFilePath { get; }
}