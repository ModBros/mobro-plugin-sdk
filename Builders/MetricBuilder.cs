using System;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="IMetric"/>
/// </summary>
public sealed class MetricBuilder :
  MetricBuilder.IIdStage,
  MetricBuilder.ILabelStage,
  MetricBuilder.ITypeStage,
  MetricBuilder.ICategoryStage,
  MetricBuilder.IGroupStage,
  MetricBuilder.IPollingStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _typeId;
  private string? _categoryId;
  private string? _groupId;
  private bool _static;
  private TimeSpan? _pollLimit;

  private MetricBuilder()
  {
  }

  internal static IIdStage CreateMetric()
  {
    return new MetricBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithId(string id)
  {
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public ITypeStage WithLabel(string label, string? description = null)
  {
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public ICategoryStage OfType(string typeId)
  {
    _typeId = typeId;
    return this;
  }

  /// <inheritdoc />
  public ICategoryStage OfType(CoreMetricType coreType)
  {
    _typeId = coreType.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public ICategoryStage OfType(IMetricType type)
  {
    _typeId = type.Id;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(string categoryId)
  {
    _categoryId = categoryId;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(CoreCategory coreCategory)
  {
    _categoryId = coreCategory.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(ICategory category)
  {
    _categoryId = category.Id;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage OfGroup(string? groupId)
  {
    _groupId = groupId;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage OfGroup(IGroup group)
  {
    _groupId = group.Id;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage OfNoGroup()
  {
    _groupId = null;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage AsStaticValue(bool @static = true)
  {
    _static = @static;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage AsDynamicValue(bool dynamic = true)
  {
    _static = !dynamic;
    return this;
  }

  /// <inheritdoc />
  public IPollingStage WithPollLimit(TimeSpan limit)
  {
    _pollLimit = limit;
    return this;
  }

  /// <inheritdoc />
  public IMetric Build()
  {
    return new Metric(
      _id ?? throw new InvalidOperationException("Metric id must not be null"),
      _label ?? throw new InvalidOperationException("Metric label must not be null"),
      _typeId ?? throw new InvalidOperationException("Metric type must not be null"),
      _categoryId ?? throw new InvalidOperationException("Category must not be null")
    )
    {
      Description = _description,
      GroupId = _groupId,
      PollLimit = _pollLimit,
      IsStatic = _static
    };
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="IMetric"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the <see cref="IMetric"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public ITypeStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface ITypeStage
  {
    /// <summary>
    /// Sets the type of the <see cref="IMetric"/> by id
    /// </summary>
    /// <param name="typeId">The id of the <see cref="IMetricType"/></param>
    /// <returns>The next building stage</returns>
    public ICategoryStage OfType(string typeId);

    /// <summary>
    /// Sets the type of the <see cref="IMetric"/> to a core type
    /// </summary>
    /// <param name="coreType">The CoreType</param>
    /// <returns>The next building stage</returns>
    public ICategoryStage OfType(CoreMetricType coreType);

    /// <summary>
    /// Sets the type of the <see cref="IMetric"/> to a
    /// <see cref="IMetricType"/>
    /// </summary>
    /// <param name="type">The <see cref="IMetricType"/></param>
    /// <returns>The next building stage</returns>
    public ICategoryStage OfType(IMetricType type);
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface ICategoryStage
  {
    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IMetric"/> belongs to
    /// </summary>
    /// <param name="categoryId">The id of the <see cref="ICategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(string categoryId);

    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IMetric"/> belongs to
    /// </summary>
    /// <param name="coreCategory">The <see cref="CoreCategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(CoreCategory coreCategory);

    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IMetric"/> belongs to
    /// </summary>
    /// <param name="category">The <see cref="ICategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(ICategory category);
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface IGroupStage
  {
    /// <summary>
    /// Sets the <see cref="IGroup"/> this <see cref="IMetric"/> belongs to. A null value indicates 'no group' and
    /// is the same as calling <see cref="OfNoGroup()"/>
    /// </summary>
    /// <param name="groupId">The id of the <see cref="IGroup"/></param>
    /// <returns>The next building stage</returns>
    public IPollingStage OfGroup(string? groupId);

    /// <summary>
    /// Sets the <see cref="IGroup"/>
    /// this <see cref="IMetric"/> belongs to
    /// </summary>
    /// <param name="group">The <see cref="IGroup"/></param>
    /// <returns>The next building stage</returns>
    public IPollingStage OfGroup(IGroup group);

    /// <summary>
    /// Builds the  <see cref="IMetric"/> without 
    /// a <see cref="IGroup"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IPollingStage OfNoGroup();
  }

  /// <summary>
  /// Building stage of the <see cref="MetricBuilder"/>
  /// </summary>
  public interface IPollingStage
  {
    /// <summary>
    /// Marks whether the value of the <see cref="IMetric"/> is static
    /// (= the value is fixed and does not change)
    /// </summary>
    /// <param name="static">Whether the value is static or not</param>
    /// <returns>The building stage</returns>
    public IPollingStage AsStaticValue(bool @static = true);

    /// <summary>
    /// Marks whether the value of the <see cref="IMetric"/> is dynamic
    /// (= the value is not fixed and will change)
    /// </summary>
    /// <param name="dynamic">Whether the value is dynomic of not</param>
    /// <returns>The building stage</returns>
    public IPollingStage AsDynamicValue(bool dynamic = true);

    /// <summary>
    /// Sets an optional poll limit for the value of the <see cref="IMetric"/>.
    /// If specified, the service will wait for at least the configured timespan
    /// before updating the metric again.
    /// </summary>
    /// <param name="limit">The poll limit</param>
    /// <returns>The building stage</returns>
    public IPollingStage WithPollLimit(TimeSpan limit);

    /// <summary>
    /// Builds and creates the <see cref="IMetric"/>.
    /// </summary>
    /// <returns>The <see cref="IMetric"/></returns>
    public IMetric Build();
  }
}