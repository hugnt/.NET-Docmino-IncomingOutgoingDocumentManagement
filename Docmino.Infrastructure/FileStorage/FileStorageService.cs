using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Models.External.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Docmino.Infrastructure.FileStorage;
public class FileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _fileStorageSettings;
    public FileStorageService(IOptions<FileStorageSettings> fileStorageSettings)
    {
        _fileStorageSettings = fileStorageSettings.Value;
    }



    public async Task<string> UploadFileAsync(IFormFile fileUploadRequest, string fileName, bool isSignedFile = false, CancellationToken cancellationToken = default)
    {
        var filePath = _fileStorageSettings.Path;
        if (!string.IsNullOrEmpty(fileName))
        {
            if (!Directory.Exists(_fileStorageSettings.Path))
            {
                Directory.CreateDirectory(_fileStorageSettings.Path);
            }
            if (isSignedFile) filePath = filePath + "/signed";
            filePath = Path.Combine(_fileStorageSettings.Path, fileName);
        }
        // Overwrite if file exists
        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await fileUploadRequest.CopyToAsync(stream);
        }
        return $"{_fileStorageSettings.BaseFilePath}/{fileName}";
    }

    public async Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
        var fullPath = Path.Combine(_fileStorageSettings.Path, filePath);
        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath), cancellationToken);
        }
    }

    public byte[] GetFile(string fileName)
    {
        var filePath = Path.Combine(_fileStorageSettings.Path, fileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {fileName}", filePath);
        }
        return File.ReadAllBytes(filePath);
    }

    public void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_fileStorageSettings.Path, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public string GetFileUrl(string fileName, bool isSignedFile = false)
    {
        return $"{_fileStorageSettings.BaseFilePath}/{fileName}";
    }

    public void SaveFile(string fileName, byte[] fileContent)
    {
        var filePath = _fileStorageSettings.Path;
        if (!string.IsNullOrEmpty(fileName))
        {
            if (!Directory.Exists(_fileStorageSettings.Path))
            {
                Directory.CreateDirectory(_fileStorageSettings.Path);
            }
            filePath = Path.Combine(_fileStorageSettings.Path, fileName);
        }
        // Overwrite if file exists
        File.WriteAllBytes(filePath, fileContent);
    }

}
