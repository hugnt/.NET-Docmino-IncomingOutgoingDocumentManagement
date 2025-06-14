using System.Net;

namespace Docmino.Application.Models;
public class Checker
{
    public bool Value { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Checker Error(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Checker
        {
            Value = false,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static Checker Success(string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new Checker
        {
            Value = true,
            Message = message,
            StatusCode = statusCode
        };
    }
}

public static class CheckerExtensions
{
    public static bool IsSuccess(this Checker checker)
    {
        return checker.Value;
    }
    public static bool IsError(this Checker checker)
    {
        return !checker.Value;
    }
}


