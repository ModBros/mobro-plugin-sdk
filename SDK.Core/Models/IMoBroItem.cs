using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models;

/// <summary>
/// An item that can be registered to the MoBro data service
/// </summary>
public interface IMoBroItem
{
  /// <summary>
  /// The id (must be unique within the scope of a plugin) 
  /// </summary>
  [Required]
  [Length(1, 128)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string Id { get; }
}