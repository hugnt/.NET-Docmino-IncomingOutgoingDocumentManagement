using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Helpers;
public static class DateHelper
{
    public static bool IsEmpty(this DateOnly? value)
    {
        // Check for null, not set, or default value (0001-01-01)
        return value == null || !value.HasValue || value.Value == default(DateOnly);
    }

    public static bool IsEmpty(this DateTime? value)
    {
        // Check for null, not set, or default value (0001-01-01 00:00:00)
        return value == null || !value.HasValue || value.Value == default(DateTime);
    }
}
