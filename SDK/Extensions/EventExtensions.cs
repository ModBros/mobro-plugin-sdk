using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MoBro.Plugin.SDK.Models.Events;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Extensions;

internal static class EventExtensions
{
  private static readonly Regex IdValidationRegex = new(@"^[\w\.\-]+$", RegexOptions.Compiled);

  internal static bool Validate(this in Event @event, IMoBroService mobroService)
  {
    // check for valid id
    if (!IdValidationRegex.IsMatch(@event.EventStreamId))
    {
      return false;
    }

    var validationContext = new ValidationContext(@event);
    var validationErrors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(@event, validationContext, validationErrors, true))
    {
      if (validationErrors.Count > 0)
      {
        return false;
      }
    }

    // check whether event stream is registered
    if (!mobroService.TryGet(@event.EventStreamId, out EventStream _))
    {
      return false;
    }

    // check whether metric is registered
    if (@event.MetricId != null && !mobroService.TryGet(@event.EventStreamId, out EventStream _))
    {
      return false;
    }

    return true;
  }
}