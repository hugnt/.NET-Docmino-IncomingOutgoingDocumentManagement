namespace Docmino.Application.Models.Responses;
public class StatisticResponse
{
    public StatisticResponse(string title, int total, string subContent)
    {
        Title = title;
        Total = total;
        SubContent = subContent;
    }

    public StatisticResponse(string title, int total)
    {
        Title = title;
        Total = total;
    }

    public string Title { get; set; }
    public int Total { get; set; }
    public string? SubContent { get; set; }
}


public class CounterQueryResult
{
    public int IncomingDocumentCount { get; set; }
    public int OutgoingDocumentCount { get; set; }
    public int InternalDocumentCount { get; set; }
    public int StoragedDocumentCount { get; set; }
}

public class DocumentStatusQueryResult
{
    public int Draff { get; set; }
    public int InProcess { get; set; }
    public int Published { get; set; }
    public int Cancel { get; set; }
}

public class MonthlyDocumentQueryResult
{
    public string Month { get; set; } = default!;
    public int IncomingDocumentCount { get; set; }
    public int OutgoingDocumentCount { get; set; }
    public int InternalDocumentCount { get; set; }
}

public class UnifiedLookupResult
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string EntityName { get; set; } = default!;
}