using Docmino.Application.Models;
using Docmino.Application.Models.Responses;

namespace Docmino.Application.Helpers;
public static class LookupHelper
{
    public static List<Lookup<T>> ToLookupList<T>(this IEnumerable<UnifiedLookupResult> source, string entityName)
    {
        return source
            .Where(x => x.EntityName == entityName)
            .Select(x => new Lookup<T>
            {
                Id = (T)(typeof(T) == typeof(Guid)
                        ? Guid.Parse(x.Id)
                        : Convert.ChangeType(x.Id, typeof(T))),
                Name = x.Name
            })
            .ToList();
    }

}
