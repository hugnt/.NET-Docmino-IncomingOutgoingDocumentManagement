using Docmino.Application.Models;

namespace Docmino.Application.Services;
public interface IStatisticService
{
    Task<Result> GetEntityCountersAsync();
    Task<Result> GetDocumentStatusCountersAsync();
    Task<Result> GetMonthlyDocumentStatisticsAsync();

}
