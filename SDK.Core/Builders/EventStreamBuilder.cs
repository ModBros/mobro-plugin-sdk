using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models.Categories;
using MoBro.Plugin.SDK.Models.Events;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="EventStream"/>
/// </summary>
public sealed class EventStreamBuilder :
  EventStreamBuilder.IIdStage,
  EventStreamBuilder.ILabelStage,
  EventStreamBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private string? _description;

  private EventStreamBuilder()
  {
  }

  internal static IIdStage CreateEventStream()
  {
    return new EventStreamBuilder();
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
  public EventStream Build()
  {
    return new EventStream
    {
      Id = _id ?? throw new ArgumentNullException(nameof(_id)),
      Label = _label ?? throw new ArgumentNullException(nameof(_label)),
      Description = _description,
    };
  }

  /// <summary>
  /// Building stage of the <see cref="EventStreamBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="EventStream"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin(.)</param>
    /// <returns>The next building stage</returns>
    ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="EventStreamBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally the description of the <see cref="EventStream"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">the optional textual description</param>
    /// <returns>The next building stage</returns>
    IBuildStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="EventStreamBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Completes and builds the <see cref="EventStream"/>
    /// </summary>
    /// <returns>The <see cref="EventStream"/></returns>
    EventStream Build();
  }
}