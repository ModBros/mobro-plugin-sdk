namespace MoBro.Plugin.SDK.Extensions;

/// <summary>
/// A static class that provides extension methods for the <see cref="string"/> type.
/// </summary>
internal static class StringExtensions
{
  /// <summary>
  /// Truncates the input string to the specified maximum length. If the string exceeds the maximum length,
  /// it is truncated and suffixed with ellipsis ("...") to indicate content has been omitted.
  /// </summary>
  /// <param name="input">The string to truncate.</param>
  /// <param name="maxLength">The maximum length of the string.</param>
  /// <returns>The truncated string if the input length exceeds the maximum length; otherwise, the original string.</returns>
  internal static string Truncate(this string input, int maxLength)
  {
    return input.Length > maxLength ? $"{input[..(maxLength - 3)]}..." : input;
  }
}