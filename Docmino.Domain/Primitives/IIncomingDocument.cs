namespace Docmino.Domain.Primitives;
public interface IIncomingDocument
{
    public string? ArrivalNumber { get; set; }
    public DateOnly? ArrivalDate { get; set; }

}
