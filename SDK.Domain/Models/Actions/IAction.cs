using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Models.Settings;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Models.Actions;

/// <summary>
/// An action represents something that is invokable at any time and then performs a certain task.
/// An action must be assigned to a registered <see cref="ICategory"/>
/// </summary>
public interface IAction : IMoBroItem
{
  /// <summary>
  /// The textual name of the action
  /// </summary>
  public string Label { get; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// The category this action is assigned to (id of a registered <see cref="ICategory"/>)
  /// </summary>
  public string CategoryId { get; }

  /// <summary>
  /// An optional group this action is part of (id of a registered <see cref="IGroup"/>)
  /// </summary>
  public string? GroupId { get; }

  /// <summary>
  /// Whether the actions returns a result or not
  /// </summary>
  public bool ReturnsResult { get; }

  /// <summary>
  /// The handler that will be called whenever this action is invoked
  /// </summary>
  public Func<IMoBroSettings, Task<object?>> Handler { get; }

  /// <summary>
  /// Settings exposed by this specific action
  /// </summary>
  public IEnumerable<SettingsFieldBase> Settings { get; }
}