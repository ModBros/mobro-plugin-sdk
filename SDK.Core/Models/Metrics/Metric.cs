using System;
using System.ComponentModel.DataAnnotations;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models.Categories;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// A metric represents a single data value provided by a plugin that will be monitored by the service.
/// A metric must be of a registered <see cref="MetricType"/> and its value must conform to the
/// <see cref="MetricValueType"/> defined in that type.
/// A metric is always assigned to a <see cref="Category"/>
/// </summary>
public sealed class Metric : IMoBroItem
{
  /// <summary>
  /// Creates a new metric.
  /// See also <see cref="MoBroItem"/> for a guided builder.
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="label">The label</param>
  /// <param name="typeId">The id of the <see cref="MetricType"/></param>
  /// <param name="categoryId">The id of the <see cref="Category"/></param>
  public Metric(string id, string label, string typeId, string categoryId)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
    TypeId = typeId ?? throw new ArgumentNullException(nameof(typeId));
    CategoryId = categoryId ?? throw new ArgumentNullException(nameof(categoryId));
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <summary>
  /// The textual name of the metric
  /// </summary>
  [Required]
  [Length(1, 32)]
  public string Label { get; set; }

  /// <summary>
  /// The type of this metric (id of a registered <see cref="MetricType"/>)
  /// </summary>
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string TypeId { get; set; }

  /// <summary>
  /// The category this metric is assigned to (id of a registered <see cref="Category"/>)
  /// </summary>
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string CategoryId { get; set; }

  /// <summary>
  /// Whether this metric is static or not. (static = the value is fixed and will not change)
  /// </summary>
  [Required]
  public bool IsStatic { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }

  /// <summary>
  /// An optional group this metric is part of (id of a registered <see cref="Group"/>)
  /// </summary>
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? GroupId { get; set; }
}