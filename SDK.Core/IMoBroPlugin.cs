using System;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK;

/// <summary>
/// A MoBro plugin. This interface must be implemented by a plugin in order for it to be loadable by MoBro.  <br/>
/// An instance of <see cref="IMoBroSettings"/> and/or <see cref="IMoBroService"/> may be injected via the plugins
/// constructor and then used throughout its lifetime.
/// </summary>
public interface IMoBroPlugin : IDisposable
{
  /// <summary>
  /// Called once upon initialization of the plugin. Any (potentially longer running) initialization code that should
  /// be run after the constructor is called can be placed here.
  /// </summary>
  public void Init()
  {
  }

  /// <summary>
  /// Same as <see cref="Init"/>, just async.
  /// </summary>
  public Task InitAsync() => Task.CompletedTask;

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