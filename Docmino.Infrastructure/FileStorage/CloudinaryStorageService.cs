using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Common.Exceptions;
using Docmino.Application.Common.Messages;
using Docmino.Application.Models.External.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Docmino.Infrastructure.FileStorage;
public class CloudinaryStorageService : IFileStorageService
{
    private readonly CloudSettings _cloudSettings;
    private readonly Cloudinary _cloudinary;

    public CloudinaryStorageService(IOptions<CloudSettings> cloudSettings)
    {
        _cloudSettings = cloudSettings.Value;
        _cloudinary = SetupCloudinary();
    }

    private Cloudinary SetupCloudinary()
    {
        var account = new Account(
            _cloudSettings.CloudName,
            _cloudSettings.ApiKey,
            _cloudSettings.ApiSecret);

        return new Cloudinary(account);
    }

    public string GetFileUrl(string fileName, bool isSignedFile = false)
    {
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        string resourceType = "";
        string folderPath = "";

        if (_cloudSettings.MediaSettings.Images.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "image";
            folderPath = _cloudSettings.MediaSettings.Images.FolderPath;
        }
        else if (_cloudSettings.MediaSettings.Videos.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "video";
            folderPath = _cloudSettings.MediaSettings.Videos.FolderPath;
        }
        else if (_cloudSettings.MediaSettings.Documents.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "raw";
            if (isSignedFile)
            {
                folderPath = _cloudSettings.MediaSettings.SignedDocuments.FolderPath;
            }
            else
            {
                folderPath = _cloudSettings.MediaSettings.Documents.FolderPath;
            }
        }
        else if (_cloudSettings.MediaSettings.Certificates.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "raw";
            folderPath = _cloudSettings.MediaSettings.Certificates.FolderPath;
        }
        else
        {
            throw new NotSupportedException("Unsupported file extension.");
        }

        string url = _cloudSettings.UrlFormat
                    .Replace("{cloudName}", _cloudSettings.CloudName)
                    .Replace("{resourceType}", resourceType)
                    .Replace("{folderPath}", folderPath)
                    .Replace("{fileName}", fileName);

        return url;
    }
    public Task<string> GetPreSignedUrlFile(string filePath, bool isDownloadUrl = false, CancellationToken token = default)
    {
        var expiresMinutes = _cloudSettings.ExpireSignatureMinutes;
        var expiresAt = DateTime.UtcNow.AddMinutes(expiresMinutes);
        var expiresAtUnixTimestamp = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();

        var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
        var format = fileExtension.Replace(".", "");

        var resourceType = "raw";
        var publicId = filePath;
        if (_cloudSettings.MediaSettings?.Images?.AllowedExtensions != null &&
            _cloudSettings.MediaSettings.Images.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "image";
            publicId = filePath.Split(".").First();
        }
        else if (_cloudSettings.MediaSettings?.Videos?.AllowedExtensions != null &&
            _cloudSettings.MediaSettings.Videos.AllowedExtensions.Contains(fileExtension))
        {
            resourceType = "video";
            publicId = filePath.Split(".").First();
        }

        var res = _cloudinary.DownloadPrivate(publicId, attachment: isDownloadUrl, resourceType: resourceType, format: format, expiresAt: expiresAtUnixTimestamp);
        return Task.FromResult(res);
    }

    public async Task<string> UploadFileAsync(IFormFile fileUploadRequest, string fileName = "", bool isSignedFile = false, CancellationToken token = default)
    {
        ValidateFile(fileUploadRequest);
        var uploadResult = await UploadMediaFileAsync(fileUploadRequest, fileName, isSignedFile, token);
        return uploadResult.SecureUrl.ToString();
    }
    public async Task DeleteFileAsync(string filePath, CancellationToken token = default)
    {
        ValidateFilePath(filePath);
        string publicId = ExtractPublicIdFromUrl(filePath);
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        CheckErrorDeleteMediaFileResult(result);
    }

    private Task<RawUploadResult> UploadMediaFileAsync(IFormFile file, string fileName, bool isSignedFile = false, CancellationToken cancellationToken = default)
    {
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (_cloudSettings.MediaSettings?.Images?.AllowedExtensions != null &&
            _cloudSettings.MediaSettings.Images.AllowedExtensions.Contains(fileExtension))
        {
            return UploadImageAsync(file, fileName, cancellationToken);
        }
        else if (_cloudSettings.MediaSettings?.Videos?.AllowedExtensions != null &&
                 _cloudSettings.MediaSettings.Videos.AllowedExtensions.Contains(fileExtension))
        {
            return UploadVideoAsync(file, fileName, cancellationToken);
        }
        else if (_cloudSettings.MediaSettings?.Documents?.AllowedExtensions != null &&
                _cloudSettings.MediaSettings.Documents.AllowedExtensions.Contains(fileExtension))
        {
            if (isSignedFile)
            {
                return UploadSignedDocumentsAsync(file, fileName, cancellationToken);
            }
            else
            {
                return UploadDocumentAsync(file, fileName, cancellationToken);
            }

        }
        else if (_cloudSettings.MediaSettings?.Certificates?.AllowedExtensions != null &&
               _cloudSettings.MediaSettings.Certificates.AllowedExtensions.Contains(fileExtension))
        {
            return UploadCertificateAsync(file, fileName, cancellationToken);
        }

        return Task.FromResult(new RawUploadResult());
    }
    private async Task<RawUploadResult> UploadImageAsync(IFormFile file, string fileName = "", CancellationToken cancellationToken = default)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = _cloudSettings.MediaSettings.Images.FolderPath,
            FilenameOverride = fileName,
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        CheckErrorUploadMediaFileResult(uploadResult);

        return uploadResult;
    }
    private async Task<RawUploadResult> UploadVideoAsync(IFormFile file, string fileName = "", CancellationToken cancellationToken = default)
    {
        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = _cloudSettings.MediaSettings.Videos.FolderPath,
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        CheckErrorUploadMediaFileResult(uploadResult);

        return uploadResult;
    }
    private async Task<RawUploadResult> UploadDocumentAsync(IFormFile file, string fileName = "", CancellationToken cancellationToken = default)
    {
        var fileNameUpload = fileName == "" ? file.FileName : fileName;
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(fileNameUpload, file.OpenReadStream()),
            Folder = _cloudSettings.MediaSettings.Documents.FolderPath,
            FilenameOverride = fileName,
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken: cancellationToken);

        CheckErrorUploadMediaFileResult(uploadResult);

        return uploadResult;
    }
    private async Task<RawUploadResult> UploadCertificateAsync(IFormFile file, string fileName = "", CancellationToken cancellationToken = default)
    {
        var fileNameUpload = fileName == "" ? file.FileName : fileName;
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(fileNameUpload, file.OpenReadStream()),
            Folder = _cloudSettings.MediaSettings.Certificates.FolderPath,
            FilenameOverride = fileName,
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken: cancellationToken);

        CheckErrorUploadMediaFileResult(uploadResult);

        return uploadResult;
    }
    private async Task<RawUploadResult> UploadSignedDocumentsAsync(IFormFile file, string fileName = "", CancellationToken cancellationToken = default)
    {
        var fileNameUpload = fileName == "" ? file.FileName : fileName;
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(fileNameUpload, file.OpenReadStream()),
            Folder = _cloudSettings.MediaSettings.SignedDocuments.FolderPath,
            FilenameOverride = fileName,
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken: cancellationToken);

        CheckErrorUploadMediaFileResult(uploadResult);

        return uploadResult;
    }



    private static void ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException(ExceptionMessage.InvalidFile);
        }
    }
    public Task<string> GetPreSignedUrlFile(string filePath, CancellationToken token = default)
    {
        ValidateFilePath(filePath);
        return Task.FromResult(filePath);
    }
    public string GetOriginFilePathFromFileSignedPath(string filePathSigned)
    {
        return filePathSigned;
    }
    private static void ValidateFilePath(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException(ExceptionMessage.InvalidFilePath);
        }
    }

    private static void CheckErrorUploadMediaFileResult(RawUploadResult uploadResult)
    {
        if (uploadResult.Error != null)
        {
            throw new UploadFileException(string.Format(ExceptionMessage.ErrorWhenUpload, uploadResult.Error.Message));
        }
    }
    private static void CheckErrorDeleteMediaFileResult(DeletionResult deleteResult)
    {
        if (deleteResult.Error != null)
        {
            throw new UploadFileException(string.Format(ExceptionMessage.ErrorWhenDeletingFile, deleteResult.Error.Message));
        }
    }
    private static string ExtractPublicIdFromUrl(string url)
    {
        try
        {
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split('/');

            int uploadIndex = Array.IndexOf(pathSegments, "upload");
            if (uploadIndex >= 0 && pathSegments.Length > uploadIndex + 1)
            {
                string fileName = Path.GetFileNameWithoutExtension(pathSegments[pathSegments.Length - 1]);

                var publicIdSegments = pathSegments
                    .Skip(uploadIndex + 1)
                    .Take(pathSegments.Length - uploadIndex - 2)
                    .ToList();

                publicIdSegments.Add(fileName);
                return string.Join("/", publicIdSegments);
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException(string.Format(ExceptionMessage.CouldNotExtractPublicId, url), ex);
        }

        throw new ArgumentException(string.Format(ExceptionMessage.CouldNotExtractPublicId, url));
    }



}
