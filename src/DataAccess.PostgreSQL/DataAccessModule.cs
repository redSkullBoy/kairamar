using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces.DataAccess;
using Utils.Modules;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccess.PostgreSQL;

public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        PostgreSQLOptions pgOptions = new();

        Configuration!.GetSection(nameof(PostgreSQLOptions))
            .Bind(pgOptions);

        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseNpgsql(pgOptions.Connection)
            .UseSnakeCaseNamingConvention());

        services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddTransient<ITokenClaimsService, IdentityTokenClaimService>();
    }
}
