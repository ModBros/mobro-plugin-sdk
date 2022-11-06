using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="ICategory"/>
/// </summary>
public sealed class CategoryBuilder :
  CategoryBuilder.IIdStage,
  CategoryBuilder.ILabelStage,
  CategoryBuilder.IIconStage,
  CategoryBuilder.ISubCategoryStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _icon;
  private readonly List<ICategory> _subCategories = new();

  private CategoryBuilder()
  {
  }

  internal static IIdStage CreateCategory()
  {
    return new CategoryBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithId(string id)
  {
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public IIconStage WithLabel(string label, string? description = null)
  {
    _label = label;
    _description = description;
    return this;
  }


  /// <inheritdoc />
  public ISubCategoryStage WithIcon(string? iconId)
  {
    _icon = iconId;
    return this;
  }

  /// <inheritdoc />
  public ISubCategoryStage WithoutIcon()
  {
    _icon = null;
    return this;
  }

  /// <inheritdoc />
  public ISubCategoryStage WithSubCategory(Func<IIdStage, ICategory> builder)
  {
    _subCategories.Add(builder.Invoke(new CategoryBuilder()));
    return this;
  }

  /// <inheritdoc />
  public ICategory Build()
  {
    return new Category(
      _id ?? throw new InvalidOperationException("Category id must not be null"),
      _label ?? throw new InvalidOperationException("Category label must not be null"),
      _description,
      _icon
    )
    {
      SubCategories = _subCategories
    };
  }

  /// <summary>
  /// Building stage of the <see cref="CategoryBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="ICategory"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="CategoryBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally the description of the <see cref="ICategory"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    IIconStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="CategoryBuilder"/>
  /// </summary>
  public interface IIconStage
  {
    /// <summary>
    /// Sets the icon of the <see cref="ICategory"/>
    /// </summary>
    /// <param name="iconId">The icon id</param>
    /// <returns>The next building stage</returns>
    ISubCategoryStage WithIcon(string? iconId);

    /// <summary>
    /// Builds the <see cref="ICategory"/> without an icon
    /// </summary>
    /// <returns>The next building stage</returns>
    ISubCategoryStage WithoutIcon();
  }

  /// <summary>
  /// Building stage of the <see cref="CategoryBuilder"/>
  /// </summary>
  public interface ISubCategoryStage
  {
    /// <summary>
    /// Adds a subcategory to the <see cref="ICategory"/>
    /// </summary>
    /// <param name="builder">The builder function for the subcategory</param>
    /// <returns>The building stage</returns>
    ISubCategoryStage WithSubCategory(Func<IIdStage, ICategory> builder);

    /// <summary>
    /// Completes and builds the <see cref="ICategory"/>
    /// </summary>
    /// <returns>The <see cref="ICategory"/></returns>
    ICategory Build();
  }
}