using Domain.Entities.AppDb;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Sqlite;

public class AppDbContext : IdentityDbContext<AppUser>, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
    {
        //Database.EnsureDeleted();   // удаляем бд со старой схемой
        //Database.EnsureCreated();   // создаем бд с новой схемой
    }

    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<Email> Emails => Set<Email>();
}
