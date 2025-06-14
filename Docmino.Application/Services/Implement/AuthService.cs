using Docmino.Application.Abstractions.Authentication;
using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Common.Enums;
using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers.Hashing;
using Docmino.Application.Helpers.Token;
using Docmino.Application.Models;
using Docmino.Application.Models.External;
using Docmino.Application.Models.Mappings;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Net;
using System.Security.Claims;

namespace Docmino.Application.Services.Implement;
public class AuthService : IAuthService
{

    private readonly IRepository<User> _userRepository;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IUserContext _userContext;
    private readonly IAuthenticationService _authenticationService;
    public AuthService(IRepository<User> userRepository, IValidator<LoginRequest> loginValidator,
                       IRepository<RefreshToken> refreshTokenRepository,
                       IUserContext userContext, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _loginValidator = loginValidator;
        _refreshTokenRepository = refreshTokenRepository;
        _userContext = userContext;
        _authenticationService = authenticationService;
    }

    public async Task<Result> Login(LoginRequest loginRequest)
    {
        var validateResult = _loginValidator.Validate(loginRequest);
        if (!validateResult.IsValid)
        {
            return Result.ErrorValidation(validateResult);
        }
        var selectedAccount = await _userRepository.FirstOrDefaultAsync(x => x.Username == loginRequest.Username && !x.IsDeleted, UserMapping.SelectAdapterExpression);
        if (selectedAccount == null)
        {
            return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(loginRequest.Username, "User"));
        }
        if (!loginRequest.Password.IsValidWith(selectedAccount.PasswordHash))
        {
            return Result.Error(HttpStatusCode.Unauthorized, AuthMessage.PasswordNotCorrect);
        }

        var claims = GetClaims(new User()
        {
            Id = selectedAccount.Id,
            Username = selectedAccount.Username,
            RoleId = selectedAccount.RoleId
        });
        var token = _authenticationService.GenerateTokens(claims);
        _refreshTokenRepository.Add(new RefreshToken()
        {
            JwtId = token.JwtId,
            UserId = selectedAccount.Id,
            Token = token.RefreshToken,
            IsUsed = false,
            IsRevoked = false,
            IssuedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddMinutes(_authenticationService.GetExpirationTimeOfRefreshToken())
        });
        await _refreshTokenRepository.SaveChangesAsync();

        var loginResponse = new LoginResponse()
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            User = selectedAccount.ToResponse()
        };

        return Result<LoginResponse>.SuccessWithBody(loginResponse);
    }
    public async Task<Result> Logout(LogoutRequest logoutRequest)
    {
        var accessToken = _userContext.AccessToken;
        var claimsFromToken = _authenticationService.GetPrincipalFromToken(accessToken!).Claims;
        var jwtId = claimsFromToken.ExtractClaimValue(ClaimType.Jti, Convert.ToString)!;
        var validateRefreshToken = await ValidateRefreshToken(new TokenModel(jwtId, accessToken, logoutRequest.RefreshToken));
        if (!validateRefreshToken.IsSuccess)
        {
            return Result.Error(HttpStatusCode.Unauthorized, validateRefreshToken.ErrorMessage);
        }

        //Update status of token 
        var selectedRefreshToken = validateRefreshToken.AttachData!;
        selectedRefreshToken.IsRevoked = true;
        selectedRefreshToken.IsUsed = true;
        _refreshTokenRepository.Update(selectedRefreshToken);
        await _refreshTokenRepository.SaveChangesAsync();
        return Result.SuccessNoContent();
    }
    public async Task<Result> ExtendSession(ExtendSessionRequest extendSessionRequest)
    {
        var accessToken = _userContext.AccessToken;
        if (accessToken == null)
        {
            return Result.Error(HttpStatusCode.Unauthorized, ErrorMessage.UserHasNoPermission);
        }
        var validateAccessToken = _authenticationService.ValidateAccessToken(accessToken);
        if (validateAccessToken.IsSuccess)
        {
            return Result<TokenResponse>.SuccessWithBody(new()
            {
                AccessToken = accessToken,
                RefreshToken = extendSessionRequest.RefreshToken,
            });
        }
        if (validateAccessToken.ErrorCode != TokenErrorCode.TokenExpired)
        {
            return Result.Error(HttpStatusCode.Unauthorized, validateAccessToken.ErrorMessage);
        }
        var claimsFromToken = _authenticationService.GetPrincipalFromToken(accessToken).Claims;
        var jwtId = claimsFromToken.ExtractClaimValue(ClaimType.Jti, Convert.ToString)!;
        var validateRefreshToken = await ValidateRefreshToken(new TokenModel(jwtId, accessToken, extendSessionRequest.RefreshToken));
        if (!validateRefreshToken.IsSuccess)
        {
            return Result.Error(HttpStatusCode.Unauthorized, validateRefreshToken.ErrorMessage);
        }

        //Update status of token 
        var selectedRefreshToken = validateRefreshToken.AttachData!;
        selectedRefreshToken.IsRevoked = true;
        selectedRefreshToken.IsUsed = true;
        _refreshTokenRepository.Update(selectedRefreshToken);

        //Create new token
        var userId = claimsFromToken.ExtractClaimValue(ClaimType.UserId, Guid.Parse)!;
        var userName = claimsFromToken.ExtractClaimValue(ClaimType.Username, Convert.ToString)!;
        var roleId = claimsFromToken.ExtractClaimValue(ClaimType.Role, Convert.ToInt32)!;
        var userClaims = new User()
        {
            Id = userId,
            Username = userName!,
            RoleId = roleId
        };
        var claims = GetClaims(userClaims);
        var token = _authenticationService.GenerateTokens(claims);

        //add new refesh token
        _refreshTokenRepository.Add(new RefreshToken()
        {
            JwtId = token.JwtId,
            UserId = userId,
            Token = token.RefreshToken,
            IsUsed = false,
            IsRevoked = false,
            IssuedAt = DateTime.UtcNow,
            ExpireAt = DateTime.UtcNow.AddMinutes(_authenticationService.GetExpirationTimeOfRefreshToken())
        });
        await _refreshTokenRepository.SaveChangesAsync();

        var tokenResponse = new TokenResponse()
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
        };
        return Result<TokenResponse>.SuccessWithBody(tokenResponse);

    }
    public async Task<Result> GetCurrentUserContext()
    {
        var currentUserId = _userContext.UserId;
        var selectedUserEntity = await _userRepository.FirstOrDefaultAsync(x => x.Id == currentUserId, UserMapping.SelectResponseExpression);
        if (selectedUserEntity == null)
        {
            return Result.Error(HttpStatusCode.Unauthorized, ErrorMessage.ObjectNotFound(currentUserId, "User"));
        }
        return Result<UserResponse>.SuccessWithBody(selectedUserEntity);
    }

    private Claim[] GetClaims(User user)
    {
        return [
            new Claim(ClaimType.Jti , Guid.NewGuid().ToString()),
            new Claim(ClaimType.Username, user.Username),
            new Claim(ClaimType.UserId , user.Id.ToString()),
            new Claim(ClaimType.Role , user.RoleId.ToString())
        ];
    }
    private async Task<TokenValidationModel<RefreshToken>> ValidateRefreshToken(TokenModel tokenModel)
    {
        if (string.IsNullOrEmpty(tokenModel.RefreshToken))
        {
            return TokenValidationModel<RefreshToken>.Error("Refresh Token must not be empty!");
        }
        var selectedRefreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.Token == tokenModel.RefreshToken);
        if (selectedRefreshToken == null)
        {
            return TokenValidationModel<RefreshToken>.Error("Refesh token does not exist");
        }
        if (selectedRefreshToken.ExpireAt < DateTime.UtcNow)
        {
            return TokenValidationModel<RefreshToken>.Error("Refesh token has been revoked");
        }
        if (selectedRefreshToken.IsUsed || selectedRefreshToken.IsRevoked)
        {
            return TokenValidationModel<RefreshToken>.Error("Refesh token has been used");
        }
        if (selectedRefreshToken.JwtId != tokenModel.JwtId)
        {
            return TokenValidationModel<RefreshToken>.Error("Jwt Id does not match");
        }
        return TokenValidationModel<RefreshToken>.Success(selectedRefreshToken);

    }
}
