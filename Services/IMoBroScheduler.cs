using System;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Scheduler implementation that can be used for recurring tasks (like polling values) by the plugin.<br/>
/// Tasks will be put on halt whenever <see cref="IMoBroPlugin.Pause"/> is called on the plugin and automatically resumed
/// once <see cref="IMoBroPlugin.Resume"/> is called.
/// </summary>
public interface IMoBroScheduler
{
  /// <summary>
  /// Schedules a one-off task that will be called once.
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <param name="delay">The delay before the execution</param>
  void OneOff(Action action, TimeSpan delay);

  /// <summary>
  /// Schedules a recurring task that will be called in the given interval.
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <param name="interval">The interval</param>
  /// <param name="delay">The delay before the first execution</param>
  void Interval(Action action, TimeSpan interval, TimeSpan delay);

  /// <summary>
  /// Schedules a recurring task based on a cron string.
  /// <a href="https://www.quartz-scheduler.net/documentation/quartz-3.x/how-tos/crontrigger.html">Cron string format</a>
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <param name="cron">The cron string</param>
  /// <param name="timeZone">The timezone</param>
  /// <param name="delay">The delay before the cron task is activated</param>
  void Cron(Action action, string cron, TimeZoneInfo timeZone, TimeSpan delay);
}