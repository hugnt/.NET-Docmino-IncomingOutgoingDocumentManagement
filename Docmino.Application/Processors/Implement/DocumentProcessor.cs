using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Docmino.Domain.Enums;
using System.Linq.Expressions;

namespace Docmino.Application.Processors.Implement;
public class DocumentProcessor : IDocumentProcessor
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IRepository<ConfirmProcess> _confirmProcessRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DocumentProcessor(IDocumentRepository documentRepository,
        IUnitOfWork unitOfWork,
        IRepository<ConfirmProcess> confirmProcessRepository)
    {
        _documentRepository = documentRepository;
        _unitOfWork = unitOfWork;
        _confirmProcessRepository = confirmProcessRepository;
    }
    public async Task ProcessExpiredDocumentsAsync()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        Expression<Func<Document, bool>> queryFilter = x =>
                                x.DocumentStatus == DocumentStatus.InProcess
                                && x.Process != null
                                && x.Process.ProcessDetails != null
                                && x.Process.ProcessDetails.Any(pd =>
                                    x.Process.CurrentStepNumber == pd.StepNumber &&
                                    pd.DateEnd > today);
        var expiredDocuments = await _documentRepository.GetAllAsync(queryFilter, navigationProperties: [x => x.Process]);
        if (expiredDocuments == null || !expiredDocuments.Any())
        {
            return;
        }
        foreach (var document in expiredDocuments)
        {
            document.DocumentStatus = DocumentStatus.Cancel;
            _documentRepository.Update(document);
            var confirmProcess = document.Process;
            confirmProcess.Status = ProcessStatus.Cancelled;
            _confirmProcessRepository.Update(confirmProcess);
        }
        await _unitOfWork.SaveChangesAsync();
    }
}
