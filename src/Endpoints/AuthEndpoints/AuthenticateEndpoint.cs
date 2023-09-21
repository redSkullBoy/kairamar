using DataAccess.Sqlite;
using FastEndpoints;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Endpoints.AuthEndpoints;

public class AuthenticateEndpoint : Endpoint<AuthenticateRequest, AuthenticateResponse>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public AuthenticateEndpoint(SignInManager<AppUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }

    public override void Configure()
    {
        Get("api/authenticate");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        if(request.Username is null || request.Password is null)
        {
            AddError("Username or Password is null");

            await SendErrorsAsync();
            return;
        }

        var response = new AuthenticateResponse();
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