using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Resources;

/// <summary>
/// A small graphic image.
/// In contrast to <see cref="Image"/> an icon can be comprised of multiple files (png, jpeg, ...) for different sizes.
/// </summary>
/// <remarks>For the best visual result always provide a single SVG whenever possible.</remarks>
public sealed class Icon : IResource
{
  /// <summary>
  /// Creates a new icon
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="alt">The alternative text</param>
  /// <param name="relativeFilePaths">The relative file paths for all supported sizes</param>
  public Icon(string id, string? alt, IDictionary<IconSize, string> relativeFilePaths)
  {
    Id = id;
    Alt = alt;
    RelativeFilePaths = relativeFilePaths;
  }

  /// <summary>
  /// Creates a new icon
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="alt">The alternative text</param>
  /// <param name="relativeFilePath">The relative path to the file for the default size</param>
  public Icon(string id, string? alt, string relativeFilePath)
  {
    Id = id;
    Alt = alt;
    RelativeFilePaths = new Dictionary<IconSize, string>();
    RelativeFilePaths[IconSize.Default] = relativeFilePath;
  }

  /// <inheritdoc />
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string Id { get; set; }

  /// <inheritdoc />
  [MaxLength(256)]
  public string? Alt { get; set; }

  /// <summary>
  /// The relative paths to the files for all supported icon sizes.
  /// </summary>
  public IDictionary<IconSize, string> RelativeFilePaths { get; set; }
}