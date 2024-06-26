using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models.Categories;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="Group"/>
/// </summary>
public sealed class GroupBuilder :
  GroupBuilder.IIdStage,
  GroupBuilder.ILabelStage,
  GroupBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _icon;
  private readonly List<Group> _subGroups = new();

  private GroupBuilder()
  {
  }

  internal static IIdStage CreateGroup()
  {
    return new GroupBuilder();
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
  public IBuildStage WithSubGroup(Func<IIdStage, Group> builder)
  {
    _subGroups.Add(builder.Invoke(new GroupBuilder()));
    return this;
  }

  /// <inheritdoc />
  public Group Build()
  {
    return new Group
    {
      Id = _id ?? throw new ArgumentNullException(nameof(_id)),
      Label = _label ?? throw new ArgumentNullException(nameof(_label)),
      Description = _description,
      Icon = _icon,
      SubGroups = _subGroups
    };
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="Group"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin(.)</param>
    /// <returns>The next building stage</returns>
    ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally the description of the <see cref="Group"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">the optional textual description</param>
    /// <returns>The next building stage</returns>
    IBuildStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Adds a subgroup to the <see cref="Group"/>
    /// </summary>
    /// <param name="builder">The builder function for the subgroup</param>
    /// <returns>The building stage</returns>
    IBuildStage WithSubGroup(Func<IIdStage, Group> builder);

    /// <summary>
    /// Sets the icon of the <see cref="Group"/>
    /// </summary>
    /// <param name="iconId">The icon id</param>
    /// <returns>The next building stage</returns>
    IBuildStage WithIcon(string? iconId);

    /// <summary>
    /// Completes and builds the <see cref="Group"/>
    /// </summary>
    /// <returns>The <see cref="Group"/></returns>
    Group Build();
  }
}