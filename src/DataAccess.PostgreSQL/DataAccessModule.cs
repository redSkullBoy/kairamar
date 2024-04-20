using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces.DataAccess;
using Utils.Modules;
using Domain.Entities;

namespace DataAccess.PostgreSQL;

public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseNpgsql("Host=btohjxdmexiffmjtfnzw-postgresql.services.clever-cloud.com;Port=50013;Database=btohjxdmexiffmjtfnzw;Username=ufr94vwpfs4h0epulrds;Password=G3nd96Thlo4yObHWrAQcaQdT2pdy6L")
            .UseSnakeCaseNamingConvention());

        services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddTransient<ITokenClaimsService, IdentityTokenClaimService>();
    }
}
