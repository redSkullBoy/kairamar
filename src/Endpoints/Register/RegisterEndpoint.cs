using DataAccess.Sqlite;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace Endpoints.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<RegisterEndpoint> _logger;
    private readonly IEmailSender _emailSender;

    public RegisterEndpoint(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterEndpoint> logger,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = new RegisterResponse { Username = request?.Email, Password = request?.Password! };

        var user = new AppUser { UserName = request?.Email, Email = request?.Email };

        var result = new IdentityResult();

        try
        {
            result = await _userManager.CreateAsync(user, request?.Password!);
        }
        catch (Exception ex)
        {
            AddError(r => r.Password, ex.Message);

            await SendErrorsAsync();
            return;
        }

        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //await _emailSender.SendEmailAsync(request!.Email!, "Confirm your email",
            //    $"Please confirm your account by code = {code}.");

            //await _signInManager.SignInAsync(user, isPersistent: false);

            await SendAsync(response);
            return;
        }

        foreach (var error in result.Errors)
        {
            if (error.Code.Contains(nameof(request.Password)))
                AddError(r => r.Password, error.Description);
            else
                AddError(r => r.Email, error.Description);
        }

        await SendErrorsAsync();
        return;
    }
}
