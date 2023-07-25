using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MoBro.Plugin.SDK;

/// <summary>
/// Builder to create a local <see cref="IMoBroPlugin"/> instance for testing.
/// </summary>
public sealed class MoBroPluginBuilder
{
  private readonly Type _pluginType;
  private readonly IDictionary<string, string> _settings = new Dictionary<string, string>();
  private LogEventLevel _logLevel = LogEventLevel.Debug;
  private ILogger? _logger;

  private MoBroPluginBuilder(Type type)
  {
    _pluginType = type;
  }

  /// <summary>
  /// Creates a plugin of the given type.
  /// </summary>
  /// <typeparam name="T">The plugin type</typeparam>
  /// <returns>The builder</returns>
  public static MoBroPluginBuilder Create<T>() where T : IMoBroPlugin
  {
    return new MoBroPluginBuilder(typeof(T));
  }

  /// <summary>
  /// Sets initial settings values.
  /// </summary>
  /// <param name="settings">The initial settings values</param>
  /// <returns>The builder</returns>
  public MoBroPluginBuilder WithSettings(IDictionary<string, string> settings)
  {
    foreach (var (key, value) in Guard.Against.Null(settings))
    {
      _settings[key] = value;
    }

    return this;
  }

  /// <summary>
  /// Sets a single initial settings value.
  /// </summary>
  /// <param name="key">The key of the settings field</param>
  /// <param name="value">The settings value</param>
  /// <returns>The builder</returns>
  public MoBroPluginBuilder WithSetting(string key, string value)
  {
    _settings[Guard.Against.NullOrWhiteSpace(key)] = Guard.Against.Null(value);
    return this;
  }

  /// <summary>
  /// Sets the log level (default = Debug) of the default logger.
  /// </summary>
  /// <param name="logLevel">The log level</param>
  /// <returns>The builder</returns>
  public MoBroPluginBuilder WithLogLevel(LogEventLevel logLevel)
  {
    _logLevel = logLevel;
    return this;
  }

  /// <summary>
  /// Sets a custom logger and overrides the default one.
  /// </summary>
  /// <param name="logger">The logger to set</param>
  /// <returns>The builder</returns>
  public MoBroPluginBuilder WithLogger(ILogger logger)
  {
    _logger = Guard.Against.Null(logger);
    return this;
  }

  /// <summary>
  /// Creates a <see cref="MoBroPluginWrapper"/> and inits the <see cref="IMoBroPlugin"/>
  /// </summary>
  /// <returns>The <see cref="MoBroPluginWrapper"/></returns>
  public MoBroPluginWrapper Build()
  {
    if (_logger == null)
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Is(_logLevel)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Quartz", LogEventLevel.Warning)
        .MinimumLevel.Override("Serilog", LogEventLevel.Warning)
        .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

      _logger = LoggerFactory
        .Create(builder => { builder.AddSerilog(); })
        .CreateLogger<MoBroPluginWrapper>();
    }

    return new MoBroPluginWrapper(_pluginType, _settings, _logger);
  }
}