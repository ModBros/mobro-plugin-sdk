using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK;

/// <summary>
/// A MoBro plugin. This interface has to be implemented in order for a plugin to be loadable by the MoBro service.
/// </summary>
public interface IMoBroPlugin : IDisposable
{
  /// <summary>
  /// Called once by the service upon initialization of the plugin.
  /// </summary>
  /// <param name="settings">The current plugin settings.</param>
  /// <param name="mobro">The <see cref="IMoBro"/> implementation.</param>
  /// <returns></returns>
  public Task Init(IPluginSettings settings, IMoBro mobro);

  /// <summary>
  /// Returns the current values for the requested metrics.
  /// </summary>
  /// <param name="ids">The metric ids.</param>
  /// <returns></returns>
  public Task<IEnumerable<IMetricValue>> GetMetricValues(IList<string> ids)
  {
    return Task.FromResult(Enumerable.Empty<IMetricValue>());
  }
}