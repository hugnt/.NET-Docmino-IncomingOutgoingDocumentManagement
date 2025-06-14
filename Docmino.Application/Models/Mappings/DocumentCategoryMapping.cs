using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Models.Mappings;
public static class DocumentCategoryMapping
{
    public static DocumentCategory ToEntity(this DocumentCategoryRequest modelRequest) => new()
    {
        Name = modelRequest.Name,
        Code = modelRequest.Code,
        Description = modelRequest.Description
    };

    public static void MappingFieldFrom(this DocumentCategory trackingEntity, DocumentCategoryRequest updatedEntity)
    {
        trackingEntity.Name = updatedEntity.Name;
        trackingEntity.Code = updatedEntity.Code;
        trackingEntity.Description = updatedEntity.Description;
    }

    public static Expression<Func<DocumentCategory, DocumentCategoryResponse>> SelectResponseExpression = x => new DocumentCategoryResponse
    {
        Id = x.Id,
        Name = x.Name,
        Code = x.Code,
        Description = x.Description
    };
}
