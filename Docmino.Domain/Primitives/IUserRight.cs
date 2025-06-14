namespace Docmino.Domain.Primitives;
public interface IUserRights
{
    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }
}
