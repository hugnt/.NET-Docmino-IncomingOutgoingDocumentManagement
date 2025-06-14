using Docmino.Application.Models.Internal;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Entities;
using System.Linq.Expressions;

namespace Docmino.Application.Models.Mappings;
public static class UserMapping
{
    public static User ToEntity(this RegisterRequest registerRequest) => new()
    {
        Fullname = registerRequest.Fullname,
        Email = registerRequest.Email,
        Username = registerRequest.Username.ToLower().Trim(),
        Role = registerRequest.Role,
    };

    //public static void MappingFieldFrom(this User trackingEntity, UserUpdateRequest updatedEntity)
    //{
    //    trackingEntity.Fullname = updatedEntity.Fullname;
    //    trackingEntity.Email = updatedEntity.Email;
    //    trackingEntity.Username = updatedEntity.Username;
    //    trackingEntity.Role = updatedEntity.Role;
    //}
    public static UserResponse ToResponse(this UserAdapterModel user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Fullname = user.Fullname,
        RoleId = user.RoleId,
        RoleName = user.RoleName,
        WalletAddress = user.WalletAddress,
        ImageSignature = user.ImageSignature,
        DigitalCertificate = user.DigitalCertificate,
        PositionName = user.PositionName,
        DepartmentName = user.DepartmentName,

        CreateIncomingDocumentRight = user.CreateIncomingDocumentRight,
        CreateOutgoingDocumentRight = user.CreateOutgoingDocumentRight,
        CreateInternalDocumentRight = user.CreateInternalDocumentRight,
        InitialConfirmProcessRight = user.InitialConfirmProcessRight,
        ProcessManagerRight = user.ProcessManagerRight,
        StoreDocumentRight = user.StoreDocumentRight,
        ManageCategories = user.ManageCategories,

        Groups = user.Groups
    };

    public static UserResponse ToResponse(this User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        Fullname = user.Fullname,
        RoleId = user.RoleId,
        RoleName = user.Role.Name,
        WalletAddress = user.WalletAddress,
        ImageSignature = user.ImageSignature,
        DigitalCertificate = user.DigitalCertificate,
        PositionName = user.Position.Name,
        DepartmentName = user.Position.Department.Name,

    };

    public static Expression<Func<User, UserAdapterModel>> SelectAdapterExpression = x => new UserAdapterModel
    {
        Id = x.Id,
        Username = x.Username,
        Email = x.Email,
        Fullname = x.Fullname,
        RoleId = x.RoleId,
        RoleName = x.Role.Name,
        WalletAddress = x.WalletAddress,
        ImageSignature = x.ImageSignature,
        DigitalCertificate = x.DigitalCertificate,
        PositionName = x.Position.Name,
        DepartmentName = x.Position.Department.Name,
        PasswordHash = x.PasswordHash,

        CreateIncomingDocumentRight = x.CreateIncomingDocumentRight,
        CreateOutgoingDocumentRight = x.CreateOutgoingDocumentRight,
        CreateInternalDocumentRight = x.CreateInternalDocumentRight,
        InitialConfirmProcessRight = x.InitialConfirmProcessRight,
        ProcessManagerRight = x.ProcessManagerRight,
        StoreDocumentRight = x.StoreDocumentRight,
        ManageCategories = x.ManageCategories,

        Groups = x.UserGroups.Select(ug => ug.Group.Name).ToList()
    };

    public static Expression<Func<User, UserResponse>> SelectResponseExpression = x => new UserResponse
    {
        Id = x.Id,
        Username = x.Username,
        Email = x.Email,
        Fullname = x.Fullname,
        RoleId = x.RoleId,
        RoleName = x.Role.Name,
        DigitalCertificate = x.DigitalCertificate,
        WalletAddress = x.WalletAddress,
        ImageSignature = x.ImageSignature,
        PositionName = x.Position.Name,
        DepartmentName = x.Position.Department.Name,

        CreateIncomingDocumentRight = x.CreateIncomingDocumentRight,
        CreateOutgoingDocumentRight = x.CreateOutgoingDocumentRight,
        CreateInternalDocumentRight = x.CreateInternalDocumentRight,
        InitialConfirmProcessRight = x.InitialConfirmProcessRight,
        ProcessManagerRight = x.ProcessManagerRight,
        StoreDocumentRight = x.StoreDocumentRight,
        ManageCategories = x.ManageCategories,

        Groups = x.UserGroups.Select(ug => ug.Group.Name).ToList()

    };

    public static Expression<Func<User, UserDetailResponse>> SelectDetailResponseExpression = x => new UserDetailResponse
    {
        Id = x.Id,
        Username = x.Username,
        Email = x.Email,
        Fullname = x.Fullname,
        RoleId = x.RoleId,
        RoleName = x.Role.Name,
        PositionId = x.PositionId,
        PositionName = x.Position.Name,
        DepartmentName = x.Position.Department.Name,
        CreateIncomingDocumentRight = x.CreateIncomingDocumentRight,
        CreateOutgoingDocumentRight = x.CreateOutgoingDocumentRight,
        CreateInternalDocumentRight = x.CreateInternalDocumentRight,
        InitialConfirmProcessRight = x.InitialConfirmProcessRight,
        ProcessManagerRight = x.ProcessManagerRight,
        StoreDocumentRight = x.StoreDocumentRight,
        ManageCategories = x.ManageCategories,
        IsDeleted = x.IsDeleted,

        Groups = x.UserGroups.Select(ug => ug.Group.Name).ToList()

    };

    public static User ToEntity(this AddUserRequest userRequest) => new()
    {
        Fullname = userRequest.Fullname,
        Email = userRequest.Email,
        Username = userRequest.Username.ToLower().Trim(),
        RoleId = userRequest.RoleId,
        PositionId = userRequest.PositionId,
    };

    public static void MappingFieldFrom(this User entity, UpdateUserRequest userRequest)
    {
        entity.Fullname = userRequest.Fullname;
        entity.Email = userRequest.Email;
        entity.Username = userRequest.Username.ToLower().Trim();
        entity.RoleId = userRequest.RoleId;
        entity.PositionId = userRequest.PositionId;
        entity.IsDeleted = userRequest.IsDeleted;
    }

    public static void MappingRightFieldFrom(this User entity, AddUserRequest userRequest)
    {
        entity.CreateIncomingDocumentRight = userRequest.CreateIncomingDocumentRight;
        entity.CreateOutgoingDocumentRight = userRequest.CreateOutgoingDocumentRight;
        entity.CreateInternalDocumentRight = userRequest.CreateInternalDocumentRight;
        entity.InitialConfirmProcessRight = userRequest.InitialConfirmProcessRight;
        entity.ProcessManagerRight = userRequest.ProcessManagerRight;
        entity.StoreDocumentRight = userRequest.StoreDocumentRight;
        entity.ManageCategories = userRequest.ManageCategories;
    }

    public static void MappingRightFieldFrom(this User entity, UpdateUserRequest userRequest)
    {
        entity.CreateIncomingDocumentRight = userRequest.CreateIncomingDocumentRight;
        entity.CreateOutgoingDocumentRight = userRequest.CreateOutgoingDocumentRight;
        entity.CreateInternalDocumentRight = userRequest.CreateInternalDocumentRight;
        entity.InitialConfirmProcessRight = userRequest.InitialConfirmProcessRight;
        entity.ProcessManagerRight = userRequest.ProcessManagerRight;
        entity.StoreDocumentRight = userRequest.StoreDocumentRight;
        entity.ManageCategories = userRequest.ManageCategories;
    }

    public static void MappingRightFieldFrom(this User entity, UserRight userRequest)
    {
        entity.CreateIncomingDocumentRight = userRequest.CreateIncomingDocumentRight;
        entity.CreateOutgoingDocumentRight = userRequest.CreateOutgoingDocumentRight;
        entity.CreateInternalDocumentRight = userRequest.CreateInternalDocumentRight;
        entity.InitialConfirmProcessRight = userRequest.InitialConfirmProcessRight;
        entity.ProcessManagerRight = userRequest.ProcessManagerRight;
        entity.StoreDocumentRight = userRequest.StoreDocumentRight;
        entity.ManageCategories = userRequest.ManageCategories;
    }

    public static void ResetRightField(this User entity)
    {
        entity.CreateIncomingDocumentRight = false;
        entity.CreateOutgoingDocumentRight = false;
        entity.CreateInternalDocumentRight = false;
        entity.InitialConfirmProcessRight = false;
        entity.ProcessManagerRight = false;
        entity.StoreDocumentRight = false;
        entity.ManageCategories = false;
    }
}
