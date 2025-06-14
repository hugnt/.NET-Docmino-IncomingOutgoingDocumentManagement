using Docmino.Application.Models.Requests;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class StoragePeriodMapping
{
    public static readonly Expression<Func<StoragePeriod, StoragePeriodResponse>> SelectResponseExpression = x => new StoragePeriodResponse
    {
        Id = x.Id,
        Name = x.Name,
        YearAmount = x.YearAmount,
        Description = x.Description,
    };

    public static StoragePeriod ToEntity(this StoragePeriodRequest request)
    {
        return new StoragePeriod
        {
            Name = request.Name,
            YearAmount = request.YearAmount,
            Description = request.Description ?? "",
        };
    }

    public static void MappingFieldFrom(this StoragePeriod entity, StoragePeriodRequest request)
    {
        entity.Name = request.Name;
        entity.YearAmount = request.YearAmount;
        entity.Description = request.Description ?? "";
    }
}