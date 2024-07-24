using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Services;
using Moq;

namespace MoBro.SDK.UnitTests;

public class MoBroPersistenceManagerTests : IDisposable
{
  private readonly string _testDirectoryPath;
  private readonly MoBroPersistenceManager _manager;
  private readonly Mock<ILogger> _mockLogger;

  public MoBroPersistenceManagerTests()
  {
    _mockLogger = new Mock<ILogger>();
    _testDirectoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    _manager = new MoBroPersistenceManager(_testDirectoryPath, _mockLogger.Object);
  }

  [Fact]
  public void TestPutAndGetString()
  {
    const string key = "test";
    const string content = "Test content";

    Assert.False(_manager.Exists(key));
    _manager.Put<string>(key, content);
    Assert.True(_manager.Exists(key));
    var readContent = _manager.Get<string>(key);
    Assert.Equal(content, readContent);
  }

  [Fact]
  public void TestPutAndGetRecord()
  {
    const string key = "test";
    var metricValue = new MetricValue("testId", 42);

    Assert.False(_manager.Exists(key));
    _manager.Put(key, metricValue);
    Assert.True(_manager.Exists(key));
    var readContent = _manager.Get<MetricValue>(key);
    Assert.Equal(metricValue, readContent);
    Assert.Equal(metricValue.Id, readContent.Id);
    Assert.Equal(metricValue.Value, readContent.Value);
  }

  [Fact]
  public void TestPutAndGetMetric()
  {
    const string key = "test";
    var metric = MoBroItem.CreateMetric()
      .WithId("id1")
      .WithLabel("label")
      .OfType("type1")
      .OfCategory("category1")
      .OfNoGroup()
      .AsStaticValue()
      .Build();

    Assert.False(_manager.Exists(key));
    _manager.Put(key, metric);
    Assert.True(_manager.Exists(key));
    var readMetric = _manager.Get<Metric>(key);

    Assert.NotNull(readMetric);
    Assert.Equal("id1", readMetric.Id);
    Assert.Equal("label", readMetric.Label);
    Assert.Null(readMetric.Description);
    Assert.Equal("type1", readMetric.TypeId);
    Assert.Equal("category1", readMetric.CategoryId);
    Assert.Null(readMetric.GroupId);
    Assert.True(readMetric.IsStatic);
    Assert.Equal(metric, readMetric);
  }


  [Fact]
  public void TestPutAndGetMetricCollection()
  {
    const string key = "test";
    var metric1 = MoBroItem.CreateMetric()
      .WithId("id1")
      .WithLabel("label")
      .OfType("type1")
      .OfCategory("category1")
      .OfNoGroup()
      .AsStaticValue()
      .Build();

    var metric2 = MoBroItem.CreateMetric()
      .WithId("id2")
      .WithLabel("label2")
      .OfType("type2")
      .OfCategory("category2")
      .OfNoGroup()
      .AsDynamicValue()
      .Build();

    Assert.False(_manager.Exists(key));
    _manager.Put(key, new[] { metric1, metric2 });
    Assert.True(_manager.Exists(key));
    var readMetrics = _manager.Get<Metric[]>(key);

    Assert.NotNull(readMetrics);
    Assert.Equal(2, readMetrics.Length);

    Assert.Equal(metric1, readMetrics[0]);
    Assert.Equal("id1", readMetrics[0].Id);
    Assert.Equal("label", readMetrics[0].Label);
    Assert.Null(readMetrics[0].Description);
    Assert.Equal("type1", readMetrics[0].TypeId);
    Assert.Equal("category1", readMetrics[0].CategoryId);
    Assert.Null(readMetrics[0].GroupId);
    Assert.True(readMetrics[0].IsStatic);

    Assert.Equal(metric2, readMetrics[1]);
    Assert.Equal("id2", readMetrics[1].Id);
    Assert.Equal("label2", readMetrics[1].Label);
    Assert.Null(readMetrics[1].Description);
    Assert.Equal("type2", readMetrics[1].TypeId);
    Assert.Equal("category2", readMetrics[1].CategoryId);
    Assert.Null(readMetrics[1].GroupId);
    Assert.False(readMetrics[1].IsStatic);
  }

  [Fact]
  public void TestRemove()
  {
    const string key = "test";
    const string content = "Test content";

    Assert.False(_manager.Exists(key));
    _manager.Put<string>(key, content);
    Assert.True(_manager.Exists(key));
    _manager.Remove(key);
    Assert.False(_manager.Exists(key));
    var readContent = _manager.Get<string>(key);
    Assert.Null(readContent);
  }

  [Fact]
  public void TestClear()
  {
    _manager.Put<string>("test1", "test-data1");
    _manager.Put<string>("test2", "test-data2");

    Assert.True(_manager.Exists("test1"));
    Assert.True(_manager.Exists("test2"));

    _manager.Clear();

    Assert.False(_manager.Exists("test1"));
    Assert.False(_manager.Exists("test2"));
  }

  [Fact]
  public void TestExists()
  {
    const string key = "test";
    const string content = "Test content";

    Assert.False(_manager.Exists(key));
    _manager.Put<string>(key, content);
    Assert.True(_manager.Exists(key));
  }

  public void Dispose()
  {
    Directory.Delete(_testDirectoryPath, true);
  }
}