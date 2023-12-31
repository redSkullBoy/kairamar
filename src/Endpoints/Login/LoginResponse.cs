﻿namespace Endpoints.Auth;

public class LoginResponse
{
    public bool Result { get; set; } = false;
    public string? Token { get; set; } = null;
    public string? Username { get; set; } = null;
    public bool IsLockedOut { get; set; } = false;
    public bool IsNotAllowed { get; set; } = false;
    public bool RequiresTwoFactor { get; set; } = false;
}
