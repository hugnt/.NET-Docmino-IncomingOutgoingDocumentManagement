using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Docmino.Application.Models;

namespace Docmino.API.Filters;

public class ValidateModelAttribute : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(modelState => modelState.Errors)
                .Select(modelError => modelError.ErrorMessage);

            context.Result = new BadRequestObjectResult(Result.ErrorList(HttpStatusCode.BadRequest, errors.ToList()));
        }

        await next();
    }
}
