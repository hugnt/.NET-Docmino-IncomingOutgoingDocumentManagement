namespace Docmino.Application.Common.Exceptions;

public class ExceptionBase : Exception
{
    public ExceptionBase(string message) : base(message) { }
}

public class BadRequestException : ExceptionBase
{
    public BadRequestException(string message) : base(message)
    {
    }

}

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message)
    {
    }
}