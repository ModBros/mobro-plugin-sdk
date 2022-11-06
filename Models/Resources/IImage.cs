namespace MoBro.Plugin.SDK.Models.Resources;

/// <summary>
/// A specific type of visual resource.
/// In contrast to <see cref="IIcon"/>, an image is only one single file and hence does not support different sizes.
/// </summary>
public interface IImage : IResource
{
  /// <summary>
  /// The relative path to the image file.
  /// </summary>
  public string RelativeFilePath { get; }
}