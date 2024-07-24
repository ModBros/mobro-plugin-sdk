using System.IO;
using Microsoft.Extensions.Logging;

namespace MoBro.Plugin.SDK.Services;

internal sealed class MoBroFileManager : IMoBroFileManager
{
  private readonly ILogger _logger;
  private readonly string _storagePath;

  public MoBroFileManager(string storagePath, ILogger logger)
  {
    _logger = logger;
    _storagePath = storagePath;
    Directory.CreateDirectory(_storagePath);
  }

  public bool Exists(string fileName) => File.Exists(AbsPath(fileName));

  public void WriteText(string fileName, string content)
  {
    File.WriteAllText(AbsPath(fileName), content);
    _logger.LogDebug("Wrote {CharCount} chars to file: {File}", content.Length, fileName);
  }

  public void WriteBytes(string fileName, byte[] content)
  {
    File.WriteAllBytes(AbsPath(fileName), content);
    _logger.LogDebug("Wrote {ByteCount} bytes to file: {File}", content.Length, fileName);
  }

  public string ReadText(string fileName) => File.ReadAllText(AbsPath(fileName));

  public byte[] ReadBytes(string fileName) => File.ReadAllBytes(AbsPath(fileName));

  private string AbsPath(string file) => Path.Join(_storagePath, file);
}