﻿using Docmino.Application.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Docmino.API.Middlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        var statusCode = httpContext.Response.StatusCode;
        httpContext.Response.ContentType = "application/json";
        var errorMessage = exception.Message;

        var errorResponse = Result.Error((HttpStatusCode)statusCode, exception.Message);

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }


}
