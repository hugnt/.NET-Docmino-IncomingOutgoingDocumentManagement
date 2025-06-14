namespace Docmino.Application.Common.Exceptions;
public class UploadFileException : BadRequestException
{
    public UploadFileException(string message) : base(message)
    {
    }
}