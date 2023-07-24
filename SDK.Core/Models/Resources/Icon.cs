using System.Collections.Generic;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Resources;

/// <inheritdoc />
public sealed class Icon : IIcon
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
  public string Id { get; }

  /// <inheritdoc />
  public string? Alt { get; }

  /// <inheritdoc />
  public IDictionary<IconSize, string> RelativeFilePaths { get; }
}