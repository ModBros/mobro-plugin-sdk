namespace MoBro.Plugin.SDK.Services;

/// <summary>
/// Provides and easy way for the plugin to manage files.
/// Files are automatically stored in the plugins data directory.
/// </summary>
public interface IMoBroFileManager
{
  /// <summary>
  /// Checks if the specified file exists.
  /// </summary>
  /// <param name="fileName">The name of the file.</param>
  /// <returns><c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
  bool Exists(string fileName);

  /// <summary>
  /// Writes the given text content to the specified file.
  /// </summary>
  /// <param name="fileName">The name of the file.</param>
  /// <param name="content">The text content to be written.</param>
  void WriteText(string fileName, string content);

  /// <summary>
  /// Writes the given byte array content to the specified file.
  /// </summary>
  /// <param name="fileName">The name of the file.</param>
  /// <param name="content">The byte array content to be written.</param>
  void WriteBytes(string fileName, byte[] content);

  /// <summary>
  /// Reads the text content from the specified file.
  /// </summary>
  /// <param name="fileName">The name of the file.</param>
  /// <returns>The text content of the file.</returns>
  string ReadText(string fileName);

  /// <summary>
  /// Reads the byte array content from the specified file.
  /// </summary>
  /// <param name="fileName">The name of the file.</param>
  /// <returns>The byte array content of the file.</returns>
  byte[] ReadBytes(string fileName);
}