using Docmino.Application.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Validators;

public class DocumentCategoryValidator : AbstractValidator<DocumentCategoryRequest>
{
    public DocumentCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name of Category must not be empty!");
    }
}
