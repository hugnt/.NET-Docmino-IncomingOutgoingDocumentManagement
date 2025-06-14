using Docmino.Application.Models.External;
using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Abstractions.FileSignature;
public interface IFileSignatureService
{
    public Task<IFormFile> AddImageToPDF(SignatureModel signatureModel);

    public Task<IFormFile> AddDigitalSignature(DigitalSignatureModel digitalSignature);
}
