using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="Category"/>
/// </summary>
public sealed class CategoryBuilder :
  CategoryBuilder.IIdStage,
  CategoryBuilder.ILabelStage,
  CategoryBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _icon;
  private readonly List<Category> _subCategories = new();

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
    ArgumentNullException.ThrowIfNull(id);
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithLabel(string label, string? description = null)
  {
    ArgumentNullException.ThrowIfNull(label);
    _label = label;
    _description = description;
    return this;
  }


  /// <inheritdoc />
  public IBuildStage WithIcon(string? iconId)
  {
    _icon = iconId;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithSubCategory(Func<IIdStage, Category> builder)
  {
    _subCategories.Add(builder.Invoke(new CategoryBuilder()));
    return this;
  }

  /// <inheritdoc />
  public Category Build()
  {
    return new Category(
      _id ?? throw new ArgumentNullException(nameof(_id)),
      _label ?? throw new ArgumentNullException(nameof(_label)),
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
    /// Sets the id of the <see cref="Category"/>
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
    /// Sets the label and optionally the description of the <see cref="Category"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    IBuildStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="CategoryBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Adds a subcategory to the <see cref="Category"/>
    /// </summary>
    /// <param name="builder">The builder function for the subcategory</param>
    /// <returns>The building stage</returns>
    IBuildStage WithSubCategory(Func<IIdStage, Category> builder);
    
    /// <summary>
    /// Sets the icon of the <see cref="Category"/>
    /// </summary>
    /// <param name="iconId">The icon id</param>
    /// <returns>The next building stage</returns>
    IBuildStage WithIcon(string? iconId);

    /// <summary>
    /// Completes and builds the <see cref="Category"/>
    /// </summary>
    /// <returns>The <see cref="Category"/></returns>
    Category Build();
  }
}