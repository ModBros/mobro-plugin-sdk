using System.Threading.Tasks;

namespace MoBro.Plugin.SDK;

/// <summary>
/// A MoBro plugin. This interface must be implemented by a plugin in order for it to be loadable by MoBro.
/// </summary>
public interface IMoBroPlugin
{
  /// <summary>
  /// Called once upon initialization of the plugin. Any (potentially longer running) initialization code that should
  /// be run after the constructor call should be placed here. <br/>
  /// This is a great place to put or invoke the logic to register metrics, update metric values or register scheduler
  /// functions. This way the actual business logic (creating and updating metrics) can be separated from the plugin
  ///  creation (the constructor call).
  /// </summary>
  public void Init()
  {
  }

  /// <summary>
  /// Same as <see cref="Init"/>, just async.
  /// </summary>
  public Task InitAsync() => Task.CompletedTask;

  /// <summary>
  /// Called once to signal the plugin that it is about to be shut down. Any cleanup code or action that should
  /// be run before the plugin is shut down should be placed here.
  /// </summary>
  void Shutdown()
  {
  }

  /// <summary>
  /// Same as <see cref="Shutdown"/>, just async.
  /// </summary>
  Task ShutdownAsync() => Task.CompletedTask;
}