using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="IGroup"/>
/// </summary>
public sealed class GroupBuilder :
  GroupBuilder.IIdStage,
  GroupBuilder.ILabelStage,
  GroupBuilder.IIconStage,
  GroupBuilder.ISubGroupStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _icon;
  private readonly List<IGroup> _subGroups = new();

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
  public ISubGroupStage WithIcon(string? iconId)
  {
    _icon = iconId;
    return this;
  }

  /// <inheritdoc />
  public ISubGroupStage WithoutIcon()
  {
    _icon = null;
    return this;
  }

  /// <inheritdoc />
  public ISubGroupStage WithSubGroup(Func<IIdStage, IGroup> builder)
  {
    _subGroups.Add(builder.Invoke(new GroupBuilder()));
    return this;
  }

  /// <inheritdoc />
  public IGroup Build()
  {
    return new Group(
      _id ?? throw new InvalidOperationException("Group id must not be null"),
      _label ?? throw new InvalidOperationException("Group label must not be null"),
      _description,
      _icon
    )
    {
      SubGroups = _subGroups
    };
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="IGroup"/>
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
    /// Sets the label and optionally the description of the <see cref="IGroup"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">the optional textual description</param>
    /// <returns>The next building stage</returns>
    IIconStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface IIconStage
  {
    /// <summary>
    /// Sets the icon of the <see cref="IGroup"/>
    /// </summary>
    /// <param name="iconId">The icon id</param>
    /// <returns>The next building stage</returns>
    ISubGroupStage WithIcon(string? iconId);

    /// <summary>
    /// Builds the <see cref="IGroup"/> without an icon
    /// </summary>
    /// <returns>The next building stage</returns>
    ISubGroupStage WithoutIcon();
  }

  /// <summary>
  /// Building stage of the <see cref="GroupBuilder"/>
  /// </summary>
  public interface ISubGroupStage
  {
    /// <summary>
    /// Adds a subgroup to the <see cref="IGroup"/>
    /// </summary>
    /// <param name="builder">The builder function for the subgroup</param>
    /// <returns>The building stage</returns>
    ISubGroupStage WithSubGroup(Func<IIdStage, IGroup> builder);

    /// <summary>
    /// Completes and builds the <see cref="IGroup"/>
    /// </summary>
    /// <returns>The <see cref="IGroup"/></returns>
    IGroup Build();
  }
}