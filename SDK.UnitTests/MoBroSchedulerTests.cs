using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Services;

namespace MoBro.SDK.UnitTests;

using Moq;
using Xunit;

public class MoBroSchedulerTests
{
  private readonly Mock<ILogger> _logger;

  public MoBroSchedulerTests()
  {
    _logger = new Mock<ILogger>();
  }

  [Fact]
  public void OneOff_CallsActionAfterDelay()
  {
    var wasCalled = false;
    var action = () => { wasCalled = true; };
    var scheduler = new MoBroScheduler(_logger.Object, e => Assert.Fail(e.Message));

    scheduler.OneOff(action, TimeSpan.Zero);

    Task.Delay(100).Wait();
    Assert.True(wasCalled);
  }

  [Fact]
  public void Interval_CallsActionAtIntervals()
  {
    var callCount = 0;
    var action = () => { callCount++; };
    var scheduler = new MoBroScheduler(_logger.Object, e => Assert.Fail(e.Message));

    scheduler.Interval(action, TimeSpan.FromMilliseconds(100), TimeSpan.Zero);

    Task.Delay(500).Wait();
    Assert.True(callCount > 1);
  }


  [Fact]
  public void TestIntervalPauseAndResume()
  {
    var callCount = 0;
    var action = () => { callCount++; };
    var scheduler = new MoBroScheduler(_logger.Object, e => Assert.Fail(e.Message));

    scheduler.Interval(action, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(200));

    Task.Delay(100).Wait();
    Assert.Equal(0, callCount);

    Task.Delay(500).Wait();
    Assert.True(callCount > 1);

    scheduler.Pause();
    var countAfterPause = callCount;

    Task.Delay(500).Wait();
    Assert.Equal(countAfterPause, callCount);

    scheduler.Resume();

    Task.Delay(200).Wait();
    Assert.True(callCount > countAfterPause);
  }
}