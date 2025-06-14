using Docmino.Domain.Entities;

namespace Docmino.Application.Models.Requests;
public class RegisterRequest
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LogoutRequest
{
    public string RefreshToken { get; set; }
}


public class ExtendSessionRequest
{
    public string RefreshToken { get; set; }
}

