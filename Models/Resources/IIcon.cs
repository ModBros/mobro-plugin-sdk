using System.Collections.Generic;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Resources;

/// <summary>
/// A specific type of visual resource.
/// Ideally an icon is provided as a single SVG image.
/// However (in contrast to <see cref="IImage"/>) an icon can also be comprised of multiple files (png, jpeg, ...) for different sizes.
/// </summary>
public interface IIcon : IResource
{
  /// <summary>
  /// The relative paths to the files for all supported icon sizes.
  /// </summary>
  public IDictionary<IconSize, string> RelativeFilePaths { get; }
}