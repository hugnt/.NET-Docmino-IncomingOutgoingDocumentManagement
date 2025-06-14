extern alias BouncyCastleOrg;
using BouncyCastleOrg::Org.BouncyCastle.Crypto;
using BouncyCastleOrg::Org.BouncyCastle.Pkcs;
using BouncyCastleOrg::Org.BouncyCastle.X509;
using Docmino.Application.Abstractions.FileSignature;
using Docmino.Application.Models.External;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Docmino.Infrastructure.FileSignature;
public class FileSignatureService : IFileSignatureService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FileSignatureService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<IFormFile> AddImageToPDF(IFormFile file, IFormFile image, int page = -1, float posX = 220, float posY = 30, float w = 140f, float h = 75f)
    {
        // Validate inputs
        if (file == null || file.Length == 0)
            throw new ArgumentException("PDF file is required.", nameof(file));
        if (image == null || image.Length == 0)
            throw new ArgumentException("Image file is required.", nameof(image));

        using var outputPdfStream = new MemoryStream();

        using var pdfStream = file.OpenReadStream();
        using var reader = new PdfReader(pdfStream);

        using var stamper = new PdfStamper(reader, outputPdfStream);

        int pageNumber = page == -1 ? reader.NumberOfPages : page;
        if (pageNumber < 1 || pageNumber > reader.NumberOfPages)
            throw new ArgumentException($"Invalid page number. Must be between 1 and {reader.NumberOfPages}.", nameof(page));

        var pdfContentByte = stamper.GetOverContent(pageNumber);

        Rectangle pageSize = reader.GetPageSize(pageNumber);

        using var imageStream = image.OpenReadStream();
        var pdfImage = Image.GetInstance(imageStream);

        pdfImage.ScaleAbsolute(w, h);
        pdfImage.SetAbsolutePosition(posX, pageSize.Height - posY - h); // Adjust posY to use bottom-left origin
        pdfContentByte.AddImage(pdfImage);

        stamper.Close();
        reader.Close();

        var outputFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_modified.pdf";
        var memoryStream = new MemoryStream(outputPdfStream.ToArray());
        var resultFile = new FormFile(memoryStream, 0, memoryStream.Length, file.Name, outputFileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };

        return await Task.FromResult(resultFile);
    }

    public async Task<IFormFile> AddImageToPDF(SignatureModel signatureModel)
    {
        if (signatureModel.ImageFile == null || signatureModel.ImageFile.Length == 0)
            throw new ArgumentException("Image file is required.", nameof(signatureModel.ImageFile));

        // Download the PDF from the URL
        using var httpClient = _httpClientFactory.CreateClient();
        using var pdfResponse = await httpClient.GetAsync(signatureModel.FileUrl, HttpCompletionOption.ResponseHeadersRead);
        if (!pdfResponse.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to download PDF from {signatureModel.FileUrl}. Status: {pdfResponse.StatusCode}");

        // Create a MemoryStream for the output PDF
        using var outputPdfStream = new MemoryStream();

        // Read the PDF stream
        using var pdfStream = await pdfResponse.Content.ReadAsStreamAsync();
        using var reader = new PdfReader(pdfStream);

        // Create PdfStamper to modify the PDF
        using var stamper = new PdfStamper(reader, outputPdfStream);

        // Determine the page to add the image to
        int pageNumber = signatureModel.PageNumber == -1 ? reader.NumberOfPages : signatureModel.PageNumber;
        if (pageNumber < 1 || pageNumber > reader.NumberOfPages)
            throw new ArgumentException($"Invalid page number. Must be between 1 and {reader.NumberOfPages}.", nameof(signatureModel.PageNumber));

        // Get the PdfContentByte for the specified page
        var pdfContentByte = stamper.GetOverContent(pageNumber);

        // Get page dimensions
        Rectangle pageSize = reader.GetPageSize(pageNumber);

        // Read the image from IFormFile
        using var imageStream = signatureModel.ImageFile.OpenReadStream();
        var pdfImage = Image.GetInstance(imageStream);

        // Scale and position the image
        pdfImage.ScaleAbsolute(signatureModel.Width, signatureModel.Height);
        pdfImage.SetAbsolutePosition(signatureModel.PosX, pageSize.Height - signatureModel.PosY - signatureModel.Height); // Adjust posY to use bottom-left origin
        pdfContentByte.AddImage(pdfImage);

        // Close the stamper to finalize the PDF
        stamper.Close();
        reader.Close();

        // Create a new IFormFile from the MemoryStream
        var outputFileName = $"{Path.GetFileNameWithoutExtension(new Uri(signatureModel.FileUrl).LocalPath)}_modified.pdf";
        var memoryStream = new MemoryStream(outputPdfStream.ToArray());
        var resultFile = new FormFile(memoryStream, 0, memoryStream.Length, signatureModel.ImageFile.Name, outputFileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };

        return resultFile;
    }

    public string AddImageToPDF(string base64File, string base64Image, int page = -1, float posX = 220, float posY = 30, float w = 140f, float h = 75f)
    {
        using (var reader = new PdfReader(Convert.FromBase64String(base64File)))
        using (MemoryStream outputPdfStream = new MemoryStream())
        {
            var stamper = new PdfStamper(reader, outputPdfStream);

            var pageSize = page;
            if (pageSize == -1) pageSize = reader.NumberOfPages;
            var pdfContentByte = stamper.GetOverContent(pageSize);

            Rectangle pagesize = reader.GetPageSize(pageSize);
            var pageHeight = pagesize.Height;
            var pageWidth = pagesize.Width;

            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Convert.FromBase64String(base64Image));
            image.ScaleAbsolute(w, h);
            image.SetAbsolutePosition(pageWidth - posX, posY);// Set image position.
            pdfContentByte.AddImage(image);
            stamper.Close();

            // Convert the MemoryStream to Base64 string
            return Convert.ToBase64String(outputPdfStream.ToArray());
        }
    }

    public async Task<IFormFile> AddDigitalSignature(DigitalSignatureModel digitalSignature)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(digitalSignature.CertificateUrl))
            throw new ArgumentException("Certificate URL is required.", nameof(digitalSignature.CertificateUrl));
        if (string.IsNullOrWhiteSpace(digitalSignature.FileUrl))
            throw new ArgumentException("PDF file URL is required.", nameof(digitalSignature.FileUrl));
        if (digitalSignature.PageNumber < 1)
            throw new ArgumentException("Page number must be greater than 0.", nameof(digitalSignature.PageNumber));

        using var httpClient = _httpClientFactory.CreateClient();
        // Step 1: Download the PFX certificate
        using var pfxResponse = await httpClient.GetAsync(digitalSignature.CertificateUrl, HttpCompletionOption.ResponseHeadersRead);
        if (!pfxResponse.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to download PFX from {digitalSignature.CertificateUrl}. Status: {pfxResponse.StatusCode}");

        // Step 2: Download the PDF
        using var pdfResponse = await httpClient.GetAsync(digitalSignature.FileUrl, HttpCompletionOption.ResponseHeadersRead);
        if (!pdfResponse.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to download PDF from {digitalSignature.FileUrl}. Status: {pdfResponse.StatusCode}");

        // Step 3: Load the PDF
        using var pdfStream = await pdfResponse.Content.ReadAsStreamAsync();
        using var reader = new PdfReader(pdfStream);

        // Validate page number
        if (digitalSignature.PageNumber > reader.NumberOfPages)
            throw new ArgumentException($"Invalid page number. Must be between 1 and {reader.NumberOfPages}.", nameof(digitalSignature.PageNumber));

        // Step 4: Load the PFX certificate
        using var pfxStream = await pfxResponse.Content.ReadAsStreamAsync();
        var pfxKeyStore = new Pkcs12Store(pfxStream, digitalSignature.Password.ToCharArray());

        // Step 5: Initialize the PDF Stamper and create the signature appearance
        using var outputStream = new MemoryStream();
        var pdfStamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);

        PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
        signatureAppearance.SignDate = DateTime.Now;
        signatureAppearance.Reason = string.IsNullOrEmpty(digitalSignature.Reason) ? "Digital Signature" : digitalSignature.Reason;
        signatureAppearance.Location = digitalSignature.Location;
        string customSignatureText = $"Người kí: {digitalSignature.SignerName}\n" +
                                $"Email: {digitalSignature.SignerEmail}\n" +
                                $"Phòng ban: {digitalSignature.SignerDepartment}\n" +
                                $"Thời gian ký: {signatureAppearance.SignDate:dd/MM/yyyy HH:mm} {signatureAppearance.SignDate:zzz}";

        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        var fontProject = Assembly.GetExecutingAssembly().GetName().Name;
        string fontPath = Path.Combine(projectPath, fontProject, "Resources", "times.ttf");

        var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        signatureAppearance.Layer2Font = new Font(baseFont, 10);
        signatureAppearance.Layer2Text = customSignatureText;
        signatureAppearance.Layer4Text = "";

        // Step 6: Add signature image if provided
        if (digitalSignature.ImageFile != null && digitalSignature.ImageFile.Length > 0)
        {
            using var imageStream = digitalSignature.ImageFile.OpenReadStream();
            var signatureImage = Image.GetInstance(imageStream);
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
            signatureAppearance.SignatureGraphic = signatureImage;
        }
        else
        {
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
        }

        // Step 7: Set the signature appearance location
        float x = digitalSignature.PosX;
        float y = reader.GetPageSize(digitalSignature.PageNumber).Height - digitalSignature.PosY - digitalSignature.Height; // Adjust for bottom-left origin
        float width = digitalSignature.Width;
        float height = digitalSignature.Height;

        signatureAppearance.Acro6Layers = false;
        //signatureAppearance.Layer4Text = "Chữ kí hợp lệ";
        signatureAppearance.SetVisibleSignature(new Rectangle(x, y, x + width, y + height), digitalSignature.PageNumber, digitalSignature.SignatureKeyField);

        // Step 8: Sign the document
        var alias = pfxKeyStore.Aliases.Cast<string>().FirstOrDefault(entryAlias => pfxKeyStore.IsKeyEntry(entryAlias));
        if (alias == null)
            throw new Exception("Private key not found in the PFX certificate.");

        ICipherParameters privateKey = pfxKeyStore.GetKey(alias).Key;
        IExternalSignature pks = new PrivateKeySignature(privateKey, DigestAlgorithms.SHA256);
        MakeSignature.SignDetached(
            signatureAppearance,
            pks,
            new X509Certificate[] { pfxKeyStore.GetCertificate(alias).Certificate },
            null, null, null, 0, CryptoStandard.CMS);

        // Step 9: Create IFormFile from the signed PDF
        pdfStamper.Close();
        var outputFileName = $"{Path.GetFileNameWithoutExtension(new Uri(digitalSignature.FileUrl).LocalPath)}_signed.pdf";
        var memoryStream = new MemoryStream(outputStream.ToArray());
        var resultFile = new FormFile(memoryStream, 0, memoryStream.Length, "signedPdf", outputFileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/pdf"
        };

        return resultFile;
    }
}
