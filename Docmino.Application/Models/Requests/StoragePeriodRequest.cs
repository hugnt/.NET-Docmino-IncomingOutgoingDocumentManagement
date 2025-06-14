namespace Docmino.Application.Models.Requests;
public class StoragePeriodRequest
{
    public string Name { get; set; }
    public int YearAmount { get; set; }
    public string? Description { get; set; }
}
