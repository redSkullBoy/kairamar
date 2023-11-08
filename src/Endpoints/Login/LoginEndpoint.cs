using DataAccess.Sqlite;
using Endpoints.Auth;
using FastEndpoints;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Endpoints.Login;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public LoginEndpoint(SignInManager<AppUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }

    public override void Configure()
    {
        Get("login");
        AllowAnonymous();
    }


    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        if(request.Username is null)
        {
            AddError(r => r.Username, "Username is null");

            await SendErrorsAsync();
            return;
        }
        if (request.Password is null)
        {
            AddError(r => r.Password, "Password is null");

            await SendErrorsAsync();
            return;
        }

        var response = new LoginResponse();
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

        if (result.Succeeded)
        {
            response.Result = result.Succeeded;
            response.IsLockedOut = result.IsLockedOut;
            response.IsNotAllowed = result.IsNotAllowed;
            response.RequiresTwoFactor = result.RequiresTwoFactor;
            response.Username = request.Username;

            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username!);

            Response = response;

            await SendAsync(response);
            return;
        }

        AddError("Authentication Failed!");

        await SendErrorsAsync();
        return;
    }
}