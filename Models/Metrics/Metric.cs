using System;
using MoBro.Plugin.SDK.Builders;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <inheritdoc />
public sealed class Metric : IMetric
{
  /// <summary>
  /// Creates a new metric.
  /// See also <see cref="MoBroItem"/> for a guided builder.
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="label">The label</param>
  /// <param name="typeId">The id of the <see cref="IMetricType"/></param>
  /// <param name="categoryId">The id of the <see cref="ICategory"/></param>
  public Metric(string id, string label, string typeId, string categoryId)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
    TypeId = typeId ?? throw new ArgumentNullException(nameof(typeId));
    CategoryId = categoryId ?? throw new ArgumentNullException(nameof(categoryId));
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <inheritdoc />
  public string Label { get; set; }

  /// <inheritdoc />
  public string TypeId { get; set; }

  /// <inheritdoc />
  public string CategoryId { get; set; }

  /// <inheritdoc />
  public bool IsStatic { get; set; }

  /// <inheritdoc />
  public string? Description { get; set; }

  /// <inheritdoc />
  public string? GroupId { get; set; }
}