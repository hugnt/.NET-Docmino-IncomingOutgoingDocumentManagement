using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Common.Enums;
using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Helpers.Files;
using Docmino.Application.Helpers.Hashing;
using Docmino.Application.Helpers.Users;
using Docmino.Application.Models;
using Docmino.Application.Models.Lookups;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement;
public class UserService : IUserService
{
    private readonly IValidator<AddUserRequest> _addUserValidator;
    private readonly IValidator<UserRequest> _userValidator;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<Position> _positionRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;
    public UserService(IRepository<User> userRepository,
                       IUserContext userContext,
                       IUnitOfWork unitOfWork,
                       IFileStorageService fileStorageService,
                       IRepository<Role> roleRepository,
                       IRepository<Position> positionRepository,
                       IValidator<UserRequest> addUserValidator,
                       IValidator<UserRequest> userValidator)
    {
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
        _roleRepository = roleRepository;
        _positionRepository = positionRepository;
        _addUserValidator = addUserValidator;
        _userValidator = userValidator;
    }

    public async Task<Result> LookupApprover()
    {
        var res = await _userRepository.GetAllAsync(
            selectQuery: x => new UserLookup
            {
                Id = x.Id,
                Name = x.Fullname,
                DepartmentName = x.Position.Department.Name,
            },
            predicate: x => !x.IsDeleted && x.RoleId == (int)RolePolicy.Approver
        );
        return Result<List<UserLookup>>.SuccessWithBody(res.ToList());
    }

    public async Task<Result> GetAll(FilterRequest filter)
    {
        Expression<Func<User, bool>> queryFilter = x =>
                                       (filter.SearchValue.IsEmpty() || x.Fullname.Contains(filter.SearchValue!) || x.Username.Contains(filter.SearchValue!));
        var res = await _userRepository.GetByFilterAsync(
                            filter.PageSize, filter.PageNumber,
                            predicate: queryFilter,
                            selectQuery: UserMapping.SelectDetailResponseExpression);
        return FilterResult<List<UserDetailResponse>>.Success(res.Data.ToList(), res.TotalCount);
    }

    public async Task<Result> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Add(AddUserRequest modelRequest)
    {
        //Validate
        var validateResult = _addUserValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckAdd(modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }
        var userEntity = modelRequest.ToEntity();
        userEntity.PasswordHash = modelRequest.Password.Hash();

        if (userEntity.IsAdmin() || userEntity.IsClericalAssistant())
        {
            userEntity.MappingRightFieldFrom(modelRequest);
        }

        _userRepository.Add(userEntity);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Tài khoản"));
    }

    public async Task<Result> Update(Guid id, UpdateUserRequest modelRequest)
    {
        //Validate
        var validateResult = _userValidator.Validate(modelRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }

        //Check
        var checkResult = await CheckUpdate(id, modelRequest);
        if (checkResult.IsError())
        {
            return Result.Error(checkResult.StatusCode, checkResult.Message);
        }

        var selectedUser = await _userRepository.FirstAsync(x => x.Id == id);
        selectedUser.MappingFieldFrom(modelRequest);
        if (selectedUser.IsAdmin() || selectedUser.IsClericalAssistant())
        {
            selectedUser.MappingRightFieldFrom(modelRequest);
        }
        else
        {
            selectedUser.ResetRightField();
        }

        _userRepository.Update(selectedUser);
        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    public async Task<Result> Delete(Guid id)
    {
        var selectedUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedUser == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound("", "Người dùng"));
        }
        selectedUser.IsDeleted = true;

        _userRepository.Update(selectedUser);
        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    public async Task<Result> UpdateRights(UpdateUserRightRequest rightRequest)
    {
        foreach (var user in rightRequest.UserRights)
        {
            var selectedUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (selectedUser == null)
            {
                return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound("", "Người dùng"));
            }
            if (selectedUser.IsClericalAssistant() || selectedUser.IsAdmin())
            {
                selectedUser.MappingRightFieldFrom(user);
                _userRepository.Update(selectedUser);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessNoContent();
    }

    private async Task<Checker> CheckAdd(AddUserRequest modelRequest)
    {
        if (await _userRepository.AnyAsync(x => x.Username == modelRequest.Username.Trim().ToLower()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(modelRequest.Username, "Tên đăng nhập"));
        }
        if (await _userRepository.AnyAsync(x => x.Email == modelRequest.Email.Trim().ToLower()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(modelRequest.Email, "Email"));
        }
        if (!await _roleRepository.AnyAsync(x => x.Id == modelRequest.RoleId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(modelRequest.RoleId, "Vai trò người dùng"), HttpStatusCode.NotFound);
        }
        if (!await _positionRepository.AnyAsync(x => x.Id == modelRequest.PositionId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(modelRequest.PositionId, "Chức vụ"), HttpStatusCode.NotFound);
        }
        return Checker.Success();
    }

    private async Task<Checker> CheckUpdate(Guid id, UpdateUserRequest modelRequest)
    {
        if (!await _userRepository.AnyAsync(x => x.Id == id))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound("", "Người dùng"), HttpStatusCode.NotFound);
        }
        if (await _userRepository.AnyAsync(x => x.Id != id && x.Username == modelRequest.Username.Trim().ToLower()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(modelRequest.Username, "Tên đăng nhập"));
        }
        if (await _userRepository.AnyAsync(x => x.Id != id && x.Email == modelRequest.Email.Trim().ToLower()))
        {
            return Checker.Error(ErrorMessage.ObjectExisted(modelRequest.Email, "Email"));
        }
        if (!await _roleRepository.AnyAsync(x => x.Id == modelRequest.RoleId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(modelRequest.RoleId, "Vai trò người dùng"), HttpStatusCode.NotFound);
        }
        if (!await _positionRepository.AnyAsync(x => x.Id == modelRequest.PositionId))
        {
            return Checker.Error(ErrorMessage.ObjectNotFound(modelRequest.PositionId, "Chức vụ"), HttpStatusCode.NotFound);
        }
        return Checker.Success();
    }

    public async Task<Result> UpdateImageSignature(UpdateImageSignatureRequest imageSignatureRequest)
    {
        var currentUser = await _userRepository.FirstAsync(x => x.Id == _userContext.UserId);

        var fileUrl = "";
        var fileName = "";
        if (imageSignatureRequest.File != null)
        {
            var fileNameRaw = imageSignatureRequest.File.FileName;
            var etension = Path.GetExtension(fileNameRaw);
            var fileNameNoExt = Path.GetFileNameWithoutExtension(fileNameRaw);
            fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + etension);
            fileUrl = _fileStorageService.GetFileUrl(fileName);
        }

        try
        {
            await _userRepository.BeginTransactionAsync();

            currentUser.ImageSignature = fileUrl;
            _userRepository.Update(currentUser);
            if (imageSignatureRequest.File != null)
            {
                await _fileStorageService.UploadFileAsync(imageSignatureRequest.File, fileName);
            }
            await _unitOfWork.SaveChangesAsync();
            await _userRepository.CommitAsync();
        }
        catch (Exception)
        {
            await _userRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }

        return Result<string>.SuccessWithBody(fileUrl);
    }

    public async Task<Result> UpdateDigitalCertificate(UpdateDigitalCertificateRequest digitalCertificate)
    {
        var currentUser = await _userRepository.FirstAsync(x => x.Id == _userContext.UserId);
        var fileUrl = "";
        var fileName = "";
        if (digitalCertificate.File != null)
        {

            var fileNameRaw = digitalCertificate.File.FileName;
            var etension = Path.GetExtension(fileNameRaw);
            var fileNameNoExt = Path.GetFileNameWithoutExtension(fileNameRaw);
            fileName = FileHelper.FormatFilename(fileNameNoExt + "_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + etension);
            fileUrl = _fileStorageService.GetFileUrl(fileName);
        }

        try
        {
            await _userRepository.BeginTransactionAsync();

            currentUser.DigitalCertificate = fileUrl;
            _userRepository.Update(currentUser);
            if (digitalCertificate.File != null)
            {
                await _fileStorageService.UploadFileAsync(digitalCertificate.File, fileName);
            }
            await _unitOfWork.SaveChangesAsync();
            await _userRepository.CommitAsync();
        }
        catch (Exception)
        {
            await _userRepository.RollbackAsync();
            return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ServerError());
        }

        return Result<string>.SuccessWithBody(fileUrl);
    }

    public async Task<Result> UpdateEmail(UpdateEmailRequest emailRequest)
    {
        var currentUser = await _userRepository.FirstAsync(x => x.Id == _userContext.UserId);
        currentUser.Email = emailRequest.Email;
        _userRepository.Update(currentUser);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }

    public async Task<Result> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
    {
        var currentUser = await _userRepository.FirstAsync(x => x.Id == _userContext.UserId);
        if (!updatePasswordRequest.OldPassword.IsValidWith(currentUser.PasswordHash))
        {
            return Result.ErrorWithMessage(AuthMessage.PasswordNotCorrect);
        }
        currentUser.PasswordHash = updatePasswordRequest.NewPassword.Hash();
        _userRepository.Update(currentUser);
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessNoContent();
    }


}
