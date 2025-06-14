namespace Docmino.Application.Processors;
public interface IDocumentProcessor
{
    Task ProcessExpiredDocumentsAsync();
}
