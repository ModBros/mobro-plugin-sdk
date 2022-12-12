using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Models.Settings;

namespace MoBro.Plugin.SDK.Models.Actions;

/// <inheritdoc />
public sealed class Action : IAction
{
  /// <summary>
  /// Creates a new action.
  /// See also <see cref="MoBroItem"/> for a guided builder.
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="label">The label</param>
  /// <param name="categoryId">The id of the <see cref="ICategory"/></param>
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

  /// <inheritdoc />
  public string Label { get; set; }

  /// <inheritdoc />
  public string? Description { get; set; }

  /// <inheritdoc />
  public string CategoryId { get; set; }

  /// <inheritdoc />
  public string? GroupId { get; set; }

  /// <inheritdoc />
  public bool ReturnsResult { get; set; }

  /// <inheritdoc />
  public Func<IMoBroSettings, Task<object?>> Handler { get; set; }

  /// <inheritdoc />
  public IEnumerable<SettingsFieldBase> Settings { get; set; } = new List<SettingsFieldBase>();
}