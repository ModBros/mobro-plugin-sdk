using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Models.Categories;
using MoBro.Plugin.SDK.Models.Settings;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Models.Actions;

/// <summary>
/// An action represents something that is invokable at any time and then performs a certain task.
/// An action must be assigned to a registered <see cref="Category"/>
/// </summary>
public sealed class Action : IMoBroItem
{
  /// <summary>
  /// Creates a new action.
  /// See also <see cref="MoBroItem"/> for a guided builder.
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="label">The label</param>
  /// <param name="categoryId">The id of the <see cref="Category"/></param>
  /// <param name="handler">The handler called whenever the action is invoked</param>
  public Action(string id, string label, string categoryId, Func<IMoBroSettings, Task<object?>> handler)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
    CategoryId = categoryId ?? throw new ArgumentNullException(nameof(categoryId));
    Handler = handler ?? throw new ArgumentNullException(nameof(handler));
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <summary>
  /// The textual name of the action
  /// </summary>
  [Required]
  [Length(1, 32)]
  public string Label { get; set; }

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
  public string CategoryId { get; set; }

  /// <summary>
  /// An optional group this action is part of (id of a registered <see cref="Group"/>)
  /// </summary>
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string? GroupId { get; set; }

  /// <summary>
  /// Whether the actions returns a result or not
  /// </summary>
  public bool ReturnsResult { get; set; }

  /// <summary>
  /// The handler that will be called whenever this action is invoked
  /// </summary>
  [Required]
  public Func<IMoBroSettings, Task<object?>> Handler { get; set; }

  /// <summary>
  /// Settings exposed by this specific action
  /// </summary>
  [MaxLength(32)]
  public IEnumerable<SettingsFieldBase> Settings { get; set; } = new List<SettingsFieldBase>();
}