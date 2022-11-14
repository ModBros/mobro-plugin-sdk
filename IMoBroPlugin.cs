using System;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK;

/// <summary>
/// A MoBro plugin. This interface has to be implemented in order for a plugin to be loadable by the MoBro service.
/// </summary>
public interface IMoBroPlugin : IDisposable
{
  /// <summary>
  /// Called once by the service upon initialization of the plugin.<br/>
  /// The passed instances of <see cref="IPluginSettings"/> and <see cref="IMoBroService"/> may be stored and used throughout
  /// the whole lifetime of the plugin to register <see cref="IMoBroItem"/>s, push updated <see cref="IMetricValue"/>s, etc.
  /// at any time.
  /// </summary>
  /// <param name="settings">The current plugin settings.</param>
  /// <param name="mobro">The <see cref="IMoBroService"/> implementation.</param>
  /// <returns></returns>
  public Task Init(IPluginSettings settings, IMoBroService mobro);

  /// <summary>
  /// Called by to signal the plugin that it should pause monitoring and stop sending metric value updates, etc.<br/>
  /// This may be called due to the service switching into idle mode as no client has requested data for a prolonged time.
  /// </summary>
  /// <remarks>Any data sent while the plugin is 'paused' may be ignored by the service.</remarks>
  /// <returns></returns>
  public Task Pause();

  /// <summary>
  /// Called to signal the plugin to resume monitoring and again start sending metric value updates, etc.<br/>
  /// Will only be called if the plugin has been paused by a call to <see cref="Pause"/>.
  /// </summary>
  /// <returns></returns>
  public Task Resume();
}