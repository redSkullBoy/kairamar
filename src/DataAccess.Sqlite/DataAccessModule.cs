using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces.DataAccess;
using Utils.Modules;

namespace DataAccess.Sqlite;

public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseSqlite("Data Source=kairamar.db"));

        services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddTransient<ITokenClaimsService, IdentityTokenClaimService>();
    }
}
