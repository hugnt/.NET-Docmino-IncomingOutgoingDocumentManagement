using Docmino.Domain.Entities;
using System.Linq.Expressions;

public static class OrganizationMapping
{
    public static Organization ToEntity(this OrganizationRequest request)
    {
        return new Organization
        {
            Name = request.Name.Trim(),
            PhoneNumber = request.PhoneNumber.Trim(),
            Email = request.Email.Trim(),
            ContactPersonName = request.ContactPersonName.Trim(),
            Description = request.Description?.Trim() ?? ""
        };
    }

    public static void MappingFieldFrom(this Organization entity, OrganizationRequest request)
    {
        entity.Name = request.Name.Trim();
        entity.PhoneNumber = request.PhoneNumber.Trim();
        entity.Email = request.Email.Trim();
        entity.ContactPersonName = request.ContactPersonName.Trim();
        entity.Description = request.Description?.Trim() ?? "";
    }

    public static Expression<Func<Organization, OrganizationResponse>> SelectResponseExpression =>
        x => new OrganizationResponse
        {
            Id = x.Id,
            Name = x.Name,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            ContactPersonName = x.ContactPersonName,
            Description = x.Description
        };
}