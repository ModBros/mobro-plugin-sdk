using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace MoBro.Plugin.SDK.Services;

internal sealed class MoBroScheduler : IMoBroScheduler
{
  private const string ActionKey = "action";
  private const string ErrorKey = "error";

  private readonly Action<Exception> _errorHandler;
  private IScheduler? _scheduler;
  private readonly ILogger _logger;

  public MoBroScheduler(ILogger logger, Action<Exception> errorHandler)
  {
    _logger = logger;
    _errorHandler = errorHandler;
  }

  public void OneOff(Action action, TimeSpan delay)
  {
    _logger.LogDebug("Scheduling 'one-off' job with delay of {Delay}", delay);
    EnsureStarted().GetAwaiter().GetResult();

    var job = JobFromAction(action);
    var trigger = TriggerBuilder
      .Create()
      .StartAt(DateTimeOffset.UtcNow + delay)
      .Build();

    _scheduler?.ScheduleJob(job, trigger);
  }

  public void Interval(Action action, TimeSpan interval, TimeSpan delay)
  {
    _logger.LogDebug("Scheduling 'interval' job for interval {Interval} with a delay of {Delay}", interval, delay);
    EnsureStarted().GetAwaiter().GetResult();

    var job = JobFromAction(action);
    var trigger = TriggerBuilder
      .Create()
      .WithSimpleSchedule(scb => scb
        .WithInterval(interval)
        .RepeatForever()
        .WithMisfireHandlingInstructionNowWithRemainingCount()
      )
      .StartAt(DateTimeOffset.UtcNow + delay)
      .Build();

    _scheduler?.ScheduleJob(job, trigger);
  }

  public void Cron(Action action, string cron, TimeZoneInfo timeZone, TimeSpan delay)
  {
    _logger.LogDebug("Scheduling 'cron' job for {Cron} with a delay of {Delay}", cron, delay);
    EnsureStarted().GetAwaiter().GetResult();

    var job = JobFromAction(action);
    var trigger = TriggerBuilder
      .Create()
      .WithCronSchedule(cron, csb => csb
        .InTimeZone(timeZone)
        .WithMisfireHandlingInstructionDoNothing()
      )
      .StartAt(DateTimeOffset.UtcNow + delay)
      .Build();

    _scheduler?.ScheduleJob(job, trigger);
  }

  public void Pause()
  {
    _logger.LogDebug("Pausing scheduler");
    _scheduler?.PauseAll();
  }

  public void Resume()
  {
    _logger.LogDebug("Resume scheduler");
    _scheduler?.ResumeAll();
  }

  public void Clear()
  {
    _logger.LogDebug("Clearing scheduler");
    _scheduler?.Clear();
  }

  private async Task EnsureStarted()
  {
    _scheduler ??= await new StdSchedulerFactory().GetScheduler();

    if (!_scheduler.IsStarted)
    {
      _logger.LogDebug("Starting scheduler");
      await _scheduler.Start();
    }
  }

  private IJobDetail JobFromAction(Action action) => JobBuilder
    .Create<ActionJob>()
    .UsingJobData(new JobDataMap
    {
      { ActionKey, action },
      { ErrorKey, _errorHandler }
    })
    .DisallowConcurrentExecution()
    .Build();

  private class ActionJob : IJob
  {
    public Task Execute(IJobExecutionContext context) => Task.Run(() =>
    {
      var jobData = context.JobDetail.JobDataMap;
      try
      {
        GetObject<Action>(jobData, ActionKey)?.Invoke();
      }
      catch (Exception e)
      {
        GetObject<Action<Exception>>(jobData, ErrorKey)?.Invoke(e);
      }
    });

    private static T? GetObject<T>(JobDataMap jobDataMap, string key) where T : class
    {
      return jobDataMap.TryGetValue(key, out var value) && value is T typedValue ? typedValue : null;
    }
  }
}