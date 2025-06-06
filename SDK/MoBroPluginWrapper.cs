using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Services;
using Action = MoBro.Plugin.SDK.Models.Actions.Action;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MoBro.Plugin.SDK;

/// <summary>
/// Wrapper around a <see cref="IMoBroPlugin"/> to locally run and test a plugin without having to start up the actual
/// MoBro service.
/// </summary>
public sealed class MoBroPluginWrapper : IDisposable
{
  private readonly Type _pluginType;
  private readonly IMoBroService _moBroService;
  private readonly Lazy<IMoBroScheduler> _moBroScheduler;
  private readonly ILogger _logger;

  private readonly string _storageDir;
  private IDictionary<string, string> _settings;
  private IMoBroPlugin? _plugin;

  internal MoBroPluginWrapper(Type pluginType, IDictionary<string, string> settings, string storageDir, ILogger logger)
  {
    _pluginType = pluginType;
    _settings = settings;
    _storageDir = storageDir;
    _logger = logger;
    _moBroService = new MoBroService(_logger);
    _moBroScheduler = new Lazy<IMoBroScheduler>(() => new MoBroScheduler(_logger, _moBroService.Error));

    Init();
  }

  /// <summary>
  /// Get the actual internal <see cref="IMoBroPlugin"/> instance.
  /// </summary>
  /// <returns>The <see cref="IMoBroPlugin"/> instance</returns>
  public IMoBroPlugin? GetPlugin() => _plugin;

  /// <summary>
  /// Applies new setting to the plugin. Causes the plugin to be re-created (same effect as if user settings would
  /// be applied by the MoBro service)
  /// </summary>
  /// <param name="settings">The settings to apply</param>
  public void ApplySettings(IDictionary<string, string> settings)
  {
    _settings = Guard.Against.Null(settings);
    _logger.LogDebug("'ApplySettings' called");
    Init();
  }

  /// <summary>
  /// Gets all items currently registered by the plugin. 
  /// </summary>
  /// <returns>All currently registered items</returns>
  public IEnumerable<IMoBroItem> GetRegisteredItems()
  {
    return _moBroService.GetAll();
  }

  /// <summary>
  /// Gets all items of a specific type currently registered by the plugin.
  /// </summary>
  /// <typeparam name="T">The type to filter for</typeparam>
  /// <returns>All currently registered items of the given type</returns>
  public IEnumerable<T> GetRegisteredItems<T>() where T : IMoBroItem
  {
    return _moBroService.GetAll<T>();
  }

  /// <summary>
  /// Invokes an action with the specified id and settings.
  /// </summary>
  /// <param name="actionId">The ID of the action to invoke.</param>
  /// <param name="settings">The settings to pass to the action (optional).</param>
  public void InvokeAction(string actionId, IDictionary<string, string>? settings = null)
  {
    var action = _moBroService
      .GetAll<Action>()
      .FirstOrDefault(a => a.Id == actionId);
    if (action == null)
    {
      _logger.LogWarning("Action {ActionId} does not exist. Can not invoke", actionId);
      return;
    }

    _logger.LogDebug("Invoking action: {ActionId}", action.Id);
    action.Handler.Invoke(new MoBroSettings(settings)).GetAwaiter().GetResult();
  }

  private void Init()
  {
    if (_plugin != null)
    {
      _logger.LogInformation("Stopping and disposing current plugin instance");
      if (_moBroScheduler.IsValueCreated) _moBroScheduler.Value.Clear();
      (_plugin as IDisposable)?.Dispose();
      _moBroService.ClearRegistration();
    }

    _logger.LogInformation("Creating new plugin instance");
    _plugin = CreateInstance();
    _logger.LogInformation("Invoking 'init' function on plugin");
    _plugin.Init();
    _plugin.InitAsync().GetAwaiter().GetResult();
  }

  private IMoBroPlugin CreateInstance()
  {
    if (_pluginType.GetConstructors().Length <= 0)
    {
      throw new PluginException("Can not instantiate plugin due to missing constructor");
    }

    var paramInfos = _pluginType.GetConstructors()[0].GetParameters();
    var constructorParams = new object[paramInfos.Length];

    foreach (var parameterInfo in paramInfos)
    {
      constructorParams[parameterInfo.Position] = parameterInfo.ParameterType.FullName switch
      {
        "MoBro.Plugin.SDK.Services.IMoBroSettings" => new MoBroSettings(_settings),
        "MoBro.Plugin.SDK.Services.IMoBroService" => _moBroService,
        "MoBro.Plugin.SDK.Services.IMoBroScheduler" => _moBroScheduler.Value,
        "MoBro.Plugin.SDK.Services.IMoBroPersistenceManager" => new MoBroPersistenceManager(_storageDir, _logger),
        "MoBro.Plugin.SDK.Services.IMoBroFileManager" => new MoBroFileManager(_storageDir, _logger),
        "Microsoft.Extensions.Logging.ILogger" => _logger,
        _ => throw new PluginException("Unknown constructor parameter, can not instantiate plugin")
      };
    }

    object? instance;
    try
    {
      instance = Activator.CreateInstance(_pluginType, constructorParams);
    }
    catch (TargetInvocationException e)
    {
      if (e.InnerException == null) throw;
      throw e.InnerException;
    }

    if (instance == null)
    {
      throw new PluginException("Failed to instantiate plugin");
    }

    return (IMoBroPlugin)instance;
  }

  /// <inheritdoc />
  public void Dispose()
  {
    _plugin?.Shutdown();
    _plugin?.ShutdownAsync().GetAwaiter().GetResult();
    (_plugin as IDisposable)?.Dispose();
  }
}