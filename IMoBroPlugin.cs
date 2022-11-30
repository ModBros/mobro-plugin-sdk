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
  /// The passed instances of <see cref="IMoBroSettings"/> and <see cref="IMoBroService"/> may be stored and used throughout
  /// the whole lifetime of the plugin to register <see cref="IMoBroItem"/>s, push updated <see cref="MetricValue"/>s, etc.
  /// at any time.
  /// </summary>
  /// <remarks>Only one of either <see cref="Init"/> or <see cref="InitAsync"/> needs to be implemented.</remarks>
  /// <param name="settings">The current plugin settings.</param>
  /// <param name="service">The <see cref="IMoBroService"/> implementation.</param>
  public void Init(IMoBroSettings settings, IMoBroService service)
  {
  }

  /// <summary>
  /// Same as <see cref="Init"/>, just async.
  /// </summary>
  /// <remarks>Only one of either <see cref="Init"/> or <see cref="InitAsync"/> needs to be implemented.</remarks>
  /// <param name="settings">The current plugin settings.</param>
  /// <param name="service">The <see cref="IMoBroService"/> implementation.</param>
  public Task InitAsync(IMoBroSettings settings, IMoBroService service) => Task.CompletedTask;

  /// <summary>
  /// Called by to signal the plugin that it should pause monitoring and stop sending metric value updates, etc.<br/>
  /// This may be called due to the service switching into idle mode as no client has requested data for a prolonged time.
  /// </summary>
  /// <remarks>Any data sent while the plugin is 'paused' may be ignored by the service.</remarks>
  public void Pause()
  {
  }

  /// <summary>
  /// Called to signal the plugin to resume monitoring and again start sending metric value updates, etc.<br/>
  /// Will only be called if the plugin has been paused by a call to <see cref="Pause"/>.
  /// </summary>
  public void Resume()
  {
  }
}