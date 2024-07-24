using Microsoft.Extensions.Logging;
using MoBro.Plugin.SDK.Services;
using Moq;

namespace MoBro.SDK.UnitTests;

public class MoBroFileManagerTests : IDisposable
{
  private readonly string _testDirectoryPath;
  private readonly MoBroFileManager _moBroFileManager;

  public MoBroFileManagerTests()
  {
    Mock<ILogger> mockLogger = new();
    _testDirectoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    _moBroFileManager = new MoBroFileManager(_testDirectoryPath, mockLogger.Object);
  }

  [Fact]
  public void WriteAndReadTextTest()
  {
    const string fileName = "test.txt";
    const string content = "Test content";

    Assert.False(_moBroFileManager.Exists(fileName));

    _moBroFileManager.WriteText(fileName, content);

    Assert.True(_moBroFileManager.Exists(fileName));

    var readContent = _moBroFileManager.ReadText(fileName);

    Assert.Equal(content, readContent);
  }

  [Fact]
  public void WriteAndReadBytesTest()
  {
    const string fileName = "test.txt";
    var content = "Test content"u8.ToArray();

    Assert.False(_moBroFileManager.Exists(fileName));

    _moBroFileManager.WriteBytes(fileName, content);

    Assert.True(_moBroFileManager.Exists(fileName));

    var readContent = _moBroFileManager.ReadBytes(fileName);

    Assert.Equal(content, readContent);
  }

  public void Dispose()
  {
    Directory.Delete(_testDirectoryPath, true);
  }
}