using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Models.External;
public class SignatureModel
{
    public required string FileUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public float PosX { get; set; } = 220;
    public float PosY { get; set; } = 30;
    public float Width { get; set; } = 140f;
    public float Height { get; set; } = 75f;
    public int PageNumber { get; set; } = -1;
}


public class DigitalSignatureModel : SignatureModel
{
    public required string CertificateUrl { get; set; }

    public string SignatureKeyField { get; set; } = "siganture";
    public string SignerName { get; set; } = "";
    public string SignerEmail { get; set; } = "";
    public string SignerDepartment { get; set; } = "";

    public string Reason { get; set; } = "Kí duyệt vb";
    public string Location { get; set; } = "";
    public string Password { get; set; } = "";

}

public static class SignatureModelExtensions
{
    public static DigitalSignatureModel ToDigitalSignature(this SignatureModel model, string certificateUrl) => new DigitalSignatureModel
    {
        FileUrl = model.FileUrl,
        ImageFile = model.ImageFile,
        PosX = model.PosX,
        PosY = model.PosY,
        Width = model.Width,
        Height = model.Height,
        PageNumber = model.PageNumber,
        CertificateUrl = certificateUrl
    };
}