using Infrastructure.Implementation.Options;
using Infrastructure.Implementation.Services;
using Infrastructure.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Utils.Modules;

namespace Infrastructure.Implementation;

public class InfrastructureModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailService, EmailService>();

        //services.AddTransient<SendEmailsJob>();

        //services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
    }
}