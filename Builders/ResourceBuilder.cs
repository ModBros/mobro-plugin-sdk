using System;
using System.Collections.Generic;
using System.IO;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models.Resources;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="IResource"/>
/// </summary>
public sealed class ResourceBuilder :
  ResourceBuilder.IIdStage,
  ResourceBuilder.IAltStage,
  ResourceBuilder.ITypeStage,
  ResourceBuilder.IIconStage,
  ResourceBuilder.IImageStage
{
  private string? _id;
  private string? _alt;
  private string? _imageRelativePath;
  private Dictionary<IconSize, string>? _iconRelativePaths;

  private ResourceBuilder()
  {
  }

  internal static IIdStage CreateResource()
  {
    return new ResourceBuilder();
  }

  /// <inheritdoc />
  public IAltStage WithId(string id)
  {
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public ITypeStage WithAlt(string? alt)
  {
    _alt = alt;
    return this;
  }

  /// <inheritdoc />
  public ITypeStage WithoutAlt()
  {
    _alt = null;
    return this;
  }

  /// <inheritdoc />
  public IIconStage Icon()
  {
    return this;
  }

  /// <inheritdoc />
  public IImageStage Image()
  {
    return this;
  }

  /// <inheritdoc />
  public IImageStage FromRelativePath(string relativePath)
  {
    _imageRelativePath = relativePath;
    return this;
  }

  /// <inheritdoc />
  public IImageStage FromAbsolutePath(string absolutePath)
  {
    _imageRelativePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), absolutePath);
    return this;
  }

  /// <inheritdoc />
  public IImage Build()
  {
    return new Image(
      _id ?? throw new InvalidOperationException("Image id must not be null"),
      _alt,
      _imageRelativePath ?? throw new InvalidOperationException("Image path must not be null")
    );
  }


  /// <inheritdoc />
  public IIconStage AddFromRelativePath(string relativePath, IconSize size = IconSize.Default)
  {
    _iconRelativePaths ??= new();
    _iconRelativePaths[size] = relativePath;
    return this;
  }


  /// <inheritdoc />
  public IIconStage AddFromAbsolutePath(string absolutePath, IconSize size = IconSize.Default)
  {
    _iconRelativePaths ??= new();
    _iconRelativePaths[size] = Path.GetRelativePath(Directory.GetCurrentDirectory(), absolutePath);
    return this;
  }

  /// <inheritdoc />
  IIcon IIconStage.Build()
  {
    return new Icon(
      _id ?? throw new InvalidOperationException("Icon id must not be null"),
      _alt,
      _iconRelativePaths ?? throw new InvalidOperationException("At least one icon path must be provided")
    );
  }

  /// <summary>
  /// Building stage of the <see cref="ResourceBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="IResource"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    IAltStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="ResourceBuilder"/>
  /// </summary>
  public interface IAltStage
  {
    /// <summary>
    /// Sets the alternative text of the <see cref="IResource"/>
    /// </summary>
    /// <param name="alt">The alternative text</param>
    /// <returns>The next building stage</returns>
    ITypeStage WithAlt(string? alt);

    /// <summary>
    /// Continue without setting an alternative text
    /// </summary>
    /// <returns>The next building stage</returns>
    ITypeStage WithoutAlt();
  }

  /// <summary>
  /// Building stage of the <see cref="ResourceBuilder"/>
  /// </summary>
  public interface ITypeStage
  {
    /// <summary>
    /// Build an <see cref="IIcon"></see> resource
    /// </summary>
    /// <returns>The next building stage</returns>
    IIconStage Icon();

    /// <summary>
    /// Build an <see cref="IImage"></see> resource
    /// </summary>
    /// <returns>The next building stage</returns>
    IImageStage Image();
  }

  /// <summary>
  /// Building stage of the <see cref="ResourceBuilder"/>
  /// </summary>
  public interface IImageStage
  {
    /// <summary>
    /// Sets a relative path to the image file
    /// </summary>
    /// <param name="relativePath">The relative path</param>
    /// <returns>The building stage</returns>
    IImageStage FromRelativePath(string relativePath);

    /// <summary>
    /// Sets an absolute path to the image file
    /// </summary>
    /// <param name="absolutePath">The absolute path</param>
    /// <returns>The building stage</returns>
    IImageStage FromAbsolutePath(string absolutePath);

    /// <summary>
    /// Completes and builds the <see cref="IImage"/>
    /// </summary>
    /// <returns>The <see cref="IImage"/></returns>
    IImage Build();
  }

  /// <summary>
  /// Building stage of the <see cref="ResourceBuilder"/>
  /// </summary>
  public interface IIconStage
  {
    /// <summary>
    /// Adds an icon from a relative path
    /// </summary>
    /// <remarks>
    /// Multiple calls to this method can be chained to set different icon sizes
    /// </remarks>
    /// <param name="relativePath">The relative path to the file</param>
    /// <param name="size">The icon size (defaults to 'Default')</param>
    /// <returns>The building stage</returns>
    IIconStage AddFromRelativePath(string relativePath, IconSize size = IconSize.Default);

    /// <summary>
    /// Adds an icon from an absolute path
    /// </summary>
    /// <remarks>
    /// Multiple calls to this method can be chained to set different icon sizes
    /// </remarks>
    /// <param name="absolutePath">The absolute path to the file</param>
    /// <param name="size">The icon size (defaults to 'Default')</param>
    /// <returns>The building stage</returns>
    IIconStage AddFromAbsolutePath(string absolutePath, IconSize size = IconSize.Default);

    /// <summary>
    /// Completes and builds the <see cref="IIcon"/>
    /// </summary>
    /// <returns>The <see cref="IIcon"/></returns>
    IIcon Build();
  }
}