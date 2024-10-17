using System;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Scheduler that can be used for recurring tasks (e.g. polling sensor values).
/// </summary>
public interface IMoBroScheduler
{
  /// <summary>
  /// Schedules a one-off task that will only be called once.
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

  /// <summary>
  /// Temporarily pauses all scheduled tasks.
  /// </summary>
  public void Pause();

  /// <summary>
  /// Resumes all previously paused scheduled tasks.
  /// </summary>
  public void Resume();

  /// <summary>
  /// Clears all currently scheduled tasks from the scheduler.
  /// </summary>
  public void Clear();
}