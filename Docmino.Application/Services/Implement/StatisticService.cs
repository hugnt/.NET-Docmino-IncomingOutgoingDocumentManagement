using Docmino.Application.Models;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;

namespace Docmino.Application.Services.Implement;
public class StatisticService : IStatisticService
{
    private readonly IRepository<Document> _documentRepository;
    public StatisticService(IRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Result> GetEntityCountersAsync()
    {
        const string sql = @"
                SELECT
                    COUNT(CASE WHEN dr.RegisterType = @p0 THEN 1 END) AS IncomingDocumentCount,
                    COUNT(CASE WHEN dr.RegisterType = @p1 THEN 1 END) AS OutgoingDocumentCount,
                    COUNT(CASE WHEN dr.RegisterType = @p2 OR dr.RegisterType = @p3 THEN 1 END) AS InternalDocumentCount,
                    COUNT(CASE WHEN d.StorageId IS NOT NULL THEN 1 END) AS StoragedDocumentCount
                FROM [Document] d
                LEFT JOIN [DocumentRegister] dr ON dr.ID = d.DocumentRegisterId";

        var queryResult = await _documentRepository
                .ExecuteRawSqlSingleAsync<CounterQueryResult>(sql,
                                        (int)DocumentType.Incomming,
                                        (int)DocumentType.Outgoing,
                                        (int)DocumentType.InternalIncomming,
                                        (int)DocumentType.InternalOutgoing);
        if (queryResult == null)
        {
            return Result<List<StatisticResponse>>.SuccessWithBody([]);
        }

        var res = new List<StatisticResponse>
        {
            new("Văn bản đến", queryResult.IncomingDocumentCount, "Tổng số văn bản đến từ bên ngoài được tiếp nhận."),
            new("Văn bản đi", queryResult.OutgoingDocumentCount, "Tổng số văn bản gửi ra ngoài từ đơn vị."),
            new("Văn bản nội bộ", queryResult.InternalDocumentCount, "Bao gồm cả văn bản nội bộ đến và đi giữa các phòng ban."),
            new("Lưu trữ", queryResult.StoragedDocumentCount, "Tổng số văn bản đã được lưu trữ trong hệ thống."),
        };


        return Result<List<StatisticResponse>>.SuccessWithBody(res);
    }

    public async Task<Result> GetDocumentStatusCountersAsync()
    {
        const string sql = @"
                            SELECT
                                SUM(IIF(DocumentStatus = @p0, 1, 0)) AS Draff,
                                SUM(IIF(DocumentStatus = @p1, 1, 0)) AS InProcess,
                                SUM(IIF(DocumentStatus = @p2, 1, 0)) AS Published,
                                SUM(IIF(DocumentStatus = @p3, 1, 0)) AS Cancel
                            FROM [Document]";

        var queryResult = await _documentRepository
                .ExecuteRawSqlSingleAsync<DocumentStatusQueryResult>(sql,
                                            (int)DocumentStatus.Draff,
                                            (int)DocumentStatus.InProcess,
                                            (int)DocumentStatus.Published,
                                            (int)DocumentStatus.Cancel);
        if (queryResult == null)
        {
            return Result<List<StatisticResponse>>.SuccessWithBody([]);
        }
        var res = new List<StatisticResponse>()
        {
            new("Nháp / chờ trình ký", queryResult.Draff),
            new("Đã duyệt", queryResult.Published),
            new("Bị từ chối / hủy", queryResult.Cancel),
            new("Đang duyệt", queryResult.InProcess),
        };
        return Result<List<StatisticResponse>>.SuccessWithBody(res);
    }

    public async Task<Result> GetMonthlyDocumentStatisticsAsync()
    {
        const string sql = @"SELECT
                                FORMAT(d.CreatedAt, 'yyyy-MM') AS Month,
                                COUNT(CASE WHEN dr.RegisterType = @p0 THEN 1 END) AS IncomingDocumentCount,
                                COUNT(CASE WHEN dr.RegisterType = @p1 THEN 1 END) AS OutgoingDocumentCount,
                                COUNT(CASE WHEN dr.RegisterType = @p2 OR dr.RegisterType = @p3 THEN 1 END) AS InternalDocumentCount
                            FROM [Document] d
                            LEFT JOIN [DocumentRegister] dr ON dr.ID = d.DocumentRegisterId
                            GROUP BY FORMAT(d.CreatedAt, 'yyyy-MM')
                            ORDER BY Month";
        var queryResult = await _documentRepository
                        .ExecuteRawSqlAsync<MonthlyDocumentQueryResult>(sql,
                                                    (int)DocumentType.Incomming,
                                                    (int)DocumentType.Outgoing,
                                                    (int)DocumentType.InternalIncomming,
                                                    (int)DocumentType.InternalOutgoing);
        return Result<List<MonthlyDocumentQueryResult>>.SuccessWithBody(queryResult);
    }
}
