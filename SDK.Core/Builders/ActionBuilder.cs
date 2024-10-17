using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models.Categories;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Models.Settings;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="Models.Actions.Action"/>
/// </summary>
public sealed class ActionBuilder :
  ActionBuilder.IIdStage,
  ActionBuilder.ILabelStage,
  ActionBuilder.ICategoryStage,
  ActionBuilder.IGroupStage,
  ActionBuilder.IMetricStage,
  ActionBuilder.IHandlerStage,
  ActionBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _categoryId;
  private string? _groupId;
  private string? _metricId;
  private Func<IMoBroSettings, Task>? _handler;
  private readonly List<SettingsFieldBase> _settings = new();

  private ActionBuilder()
  {
  }

  internal static IIdStage CreateAction()
  {
    return new ActionBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithId(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public ICategoryStage WithLabel(string label, string? description = null)
  {
    ArgumentNullException.ThrowIfNull(label);
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(string? categoryId)
  {
    _categoryId = categoryId ?? CoreCategory.Miscellaneous.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(CoreCategory coreCategory)
  {
    _categoryId = coreCategory.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(Category category)
  {
    ArgumentNullException.ThrowIfNull(category);
    _categoryId = category.Id;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfNoCategory()
  {
    _categoryId = CoreCategory.Miscellaneous.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfGroup(string? groupId)
  {
    _groupId = groupId;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfGroup(Group group)
  {
    ArgumentNullException.ThrowIfNull(group);
    _groupId = group.Id;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfNoGroup()
  {
    _groupId = null;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage WithMetric(string? metricId)
  {
    _metricId = metricId;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage WithMetric(Metric metric)
  {
    ArgumentNullException.ThrowIfNull(metric);
    _metricId = metric.Id;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage WithNoMetric()
  {
    _metricId = null;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithHandler(Action<IMoBroSettings> handler)
  {
    ArgumentNullException.ThrowIfNull(handler);
    _handler = p =>
    {
      handler.Invoke(p);
      return Task.CompletedTask;
    };
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task> handler)
  {
    ArgumentNullException.ThrowIfNull(handler);
    _handler = handler;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithSetting(Func<SettingsBuilder.INameStage, SettingsFieldBase> builderFunction)
  {
    ArgumentNullException.ThrowIfNull(builderFunction);
    _settings.Add(builderFunction.Invoke(SettingsBuilder.CreateSetting()));
    return this;
  }

  /// <inheritdoc />
  public Models.Actions.Action Build()
  {
    return new Models.Actions.Action
    {
      Id = _id ?? throw new ArgumentNullException(nameof(_id)),
      Label = _label ?? throw new ArgumentNullException(nameof(_label)),
      CategoryId = _categoryId ?? CoreCategory.Miscellaneous.ToString().ToLower(),
      Handler = _handler ?? (_ => Task.CompletedTask),
      Description = _description,
      GroupId = _groupId,
      MetricId = _metricId,
      Settings = _settings
    };
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="Models.Actions.Action"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the <see cref="Models.Actions.Action"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public ICategoryStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface ICategoryStage
  {
    /// <summary>
    /// Sets the <see cref="Category"/> this <see cref="Models.Actions.Action"/> belongs to
    /// </summary>
    /// <param name="categoryId">The id of the <see cref="Category"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(string? categoryId);

    /// <summary>
    /// Sets the <see cref="Category"/> this <see cref="Models.Actions.Action"/> belongs to
    /// </summary>
    /// <param name="coreCategory">The <see cref="CoreCategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(CoreCategory coreCategory);

    /// <summary>
    /// Sets the <see cref="Category"/> this <see cref="Models.Actions.Action"/> belongs to
    /// </summary>
    /// <param name="category">The <see cref="Category"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(Category category);

    /// <summary>
    /// Sets the <see cref="Category"/> of this <see cref="Models.Actions.Action"/> to the default value of
    /// <see cref="CoreCategory.Miscellaneous"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IGroupStage OfNoCategory();
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IGroupStage
  {
    /// <summary>
    /// Sets the <see cref="Group"/> this <see cref="Models.Actions.Action"/> belongs to. A null value indicates 'no group' and
    /// is the same as calling <see cref="OfNoGroup()"/>
    /// </summary>
    /// <param name="groupId">The id of the <see cref="Group"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfGroup(string? groupId);

    /// <summary>
    /// Sets the <see cref="Group"/>
    /// this <see cref="Models.Actions.Action"/> belongs to
    /// </summary>
    /// <param name="group">The <see cref="Group"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfGroup(Group group);

    /// <summary>
    /// Builds the  <see cref="Models.Actions.Action"/> without 
    /// a <see cref="Group"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfNoGroup();
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IMetricStage
  {
    /// <summary>
    /// Sets the <see cref="Metric"/> representing the value this <see cref="Models.Actions.Action"/> adjusts or
    /// influences. A null value indicates 'no metric' and is the same as calling <see cref="WithNoMetric()"/>
    /// </summary>
    /// <param name="metricId">The id of the <see cref="Metric"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage WithMetric(string? metricId);

    /// <summary>
    /// Sets the <see cref="Metric"/> representing the value this <see cref="Models.Actions.Action"/> adjusts or
    /// influences.
    /// </summary>
    /// <param name="metric">The <see cref="Metric"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage WithMetric(Metric metric);

    /// <summary>
    /// Builds the <see cref="Models.Actions.Action"/> without a <see cref="Metric"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IHandlerStage WithNoMetric();
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IHandlerStage
  {
    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithHandler(Action<IMoBroSettings> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task> handler);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Adds a setting to the action
    /// </summary>
    /// <param name="builderFunction">The builder function for the setting</param>
    /// <returns>The building stage</returns>
    IBuildStage WithSetting(Func<SettingsBuilder.INameStage, SettingsFieldBase> builderFunction);

    /// <summary>
    /// Builds and creates the <see cref="Models.Actions.Action"/>.
    /// </summary>
    /// <returns>The <see cref="Models.Actions.Action"/></returns>
    public Models.Actions.Action Build();
  }
}