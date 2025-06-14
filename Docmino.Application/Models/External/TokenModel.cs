﻿using Docmino.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Models.External;
public class TokenModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string JwtId { get; set; }

    public TokenModel(string jwtId, string accessToken, string refreshToken)
    {
        JwtId = jwtId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}

public class TokenValidationModel<T>
{
    public bool IsSuccess { get; set; }
    public T? AttachData { get; set; }
    public string ErrorMessage { get; set; } = "";
    public TokenErrorCode ErrorCode { get; set; }
    public static TokenValidationModel<T> ErrorWithCode(TokenErrorCode errorCode, string message) => new() { IsSuccess = false, ErrorCode = errorCode, ErrorMessage = message };
    public static TokenValidationModel<T> Error(string message) => new() { IsSuccess = false, ErrorMessage = message };
    public static TokenValidationModel<T> Success(T attachData) => new() { IsSuccess = true, AttachData = attachData };
    public static TokenValidationModel<T> Success() => new() { IsSuccess = true };
}
