using Coravel.Invocable;
using Docmino.Application.Processors;

namespace Docmino.Infrastructure.BackgroundJobs;
public class DocumentExpirationProcessingJob : IInvocable
{
    private readonly IDocumentProcessor _processor;

    public DocumentExpirationProcessingJob(IDocumentProcessor processor)
    {
        _processor = processor;
    }
    public async Task Invoke()
    {
        await _processor.ProcessExpiredDocumentsAsync();
    }
}
