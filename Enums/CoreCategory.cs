namespace MoBro.Plugin.SDK.Enums;

/// <summary>
/// Global categories provided by the service. These are available across all plugins.
/// </summary>
public enum CoreCategory
{
  /// <summary>
  /// Everything that doesn't really fit a category
  /// </summary>
  Miscellaneous,

  /// <summary>
  /// Battery related items (capacity,...)
  /// </summary>
  Battery,

  /// <summary>
  /// CPU (processor) related items (load, temperature,...)
  /// </summary>
  Cpu,

  /// <summary>
  /// GPU (graphics card) related items (load, temperature,...)
  /// </summary>
  Gpu,

  /// <summary>
  /// Mainboard related items (voltage, temperature,...)
  /// </summary>
  Mainboard,

  /// <summary>
  /// Network related items (e.g. NICs, download speed,...)
  /// </summary>
  Network,

  /// <summary>
  /// RAM (memory) related items (usage, available,...)
  /// </summary>
  Ram,

  /// <summary>
  /// Storage related items (SSDs, HDDs,...)
  /// </summary>
  Storage,

  /// <summary>
  /// System related items (operating system, user,...) 
  /// </summary>
  System,

  /// <summary>
  /// Fan related items (fan speed,...) 
  /// </summary>
  Fan,

  /// <summary>
  /// Weather related items (current temperature, weather icon,...)
  /// </summary>
  Weather,

  /// <summary>
  /// Media related items (Currently playing song, music controls,...)
  /// </summary>
  Media,

  /// <summary>
  /// Items related to a specific program 
  /// </summary>
  Program,

  /// <summary>
  /// Items related to a specific game 
  /// </summary>
  Game
}