using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Abstractions.FileStorage;
public interface IFileStorageService
{
    public string GetFileUrl(string fileName, bool isSignedFile = false);
    public Task<string> UploadFileAsync(IFormFile file, string fileName, bool isSignedFile = false, CancellationToken cancellationToken = default);
    public Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
}
