using Domain.Entities.Models;
using Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Sqlite;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<Email> Emails => Set<Email>();
}
