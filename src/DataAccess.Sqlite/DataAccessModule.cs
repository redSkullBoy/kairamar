using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces.DataAccess;
using Utils.Modules;

namespace DataAccess.Sqlite;

public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseSqlite(Configuration!.GetConnectionString("SqliteConnection")));

        services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}
