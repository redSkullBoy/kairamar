using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utils.Modules;

namespace DataAccess.Sqlite;

public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseSqlite(Configuration!.GetConnectionString("SqliteConnection")));
    }
}
