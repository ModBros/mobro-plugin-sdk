using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Models.Categories;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Models.Settings;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Models.Actions;

/// <summary>
/// An action represents something that is invokable at any time and then performs a certain task.
/// An action must be assigned to a registered <see cref="Category"/>
/// </summary>
public sealed class Action : IMoBroItem
{
  /// <inheritdoc />
  public required string Id { get; set; }

  /// <summary>
  /// The textual name of the action
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string Label { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }

  /// <summary>
  /// The category this action is assigned to (id of a registered <see cref="Category"/>)
  /// </summary>
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public required string CategoryId { get; set; }

  /// <summary>
  /// An optional group this action is part of (id of a registered <see cref="Group"/>)
  /// </summary>
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? GroupId { get; set; }

  /// <summary>
  /// An optional metric that represents the value this action adjusts or influences <see cref="Metric"/>)
  /// </summary>
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? MetricId { get; set; }

  /// <summary>
  /// Whether the actions returns a result or not
  /// </summary>
  public bool ReturnsResult { get; set; }

  /// <summary>
  /// The handler that will be called whenever this action is invoked
  /// </summary>
  [Required]
  public required Func<IMoBroSettings, Task<object?>> Handler { get; set; }

  /// <summary>
  /// Settings exposed by this specific action
  /// </summary>
  [MaxLength(32)]
  public IEnumerable<SettingsFieldBase> Settings { get; set; } = new List<SettingsFieldBase>();
}