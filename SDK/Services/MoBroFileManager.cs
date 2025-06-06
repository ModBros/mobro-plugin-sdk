using System.IO;
using Microsoft.Extensions.Logging;

namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Implementation of <see cref="IMoBroFileManager"/> for local testing.
/// </summary>
public sealed class MoBroFileManager : IMoBroFileManager
{
  private readonly ILogger _logger;
  private readonly string _storagePath;

  /// <summary>
  /// Instantiates a new MoBroFileManager
  /// </summary>
  /// <param name="storagePath">The path to the storage directory</param>
  /// <param name="logger">An instance of <see cref="ILogger"/></param>
  public MoBroFileManager(string storagePath, ILogger logger)
  {
    _logger = logger;
    _storagePath = storagePath;
    Directory.CreateDirectory(_storagePath);
  }

  /// <inheritdoc />
  public bool Exists(string fileName) => File.Exists(AbsPath(fileName));

  /// <inheritdoc />
  public void WriteText(string fileName, string content)
  {
    File.WriteAllText(AbsPath(fileName), content);
    _logger.LogDebug("Wrote {CharCount} chars to file: {File}", content.Length, fileName);
  }

  /// <inheritdoc />
  public void WriteBytes(string fileName, byte[] content)
  {
    File.WriteAllBytes(AbsPath(fileName), content);
    _logger.LogDebug("Wrote {ByteCount} bytes to file: {File}", content.Length, fileName);
  }

  /// <inheritdoc />
  public string ReadText(string fileName) => File.ReadAllText(AbsPath(fileName));

  /// <inheritdoc />
  public byte[] ReadBytes(string fileName) => File.ReadAllBytes(AbsPath(fileName));

  private string AbsPath(string file) => Path.Join(_storagePath, file);
}