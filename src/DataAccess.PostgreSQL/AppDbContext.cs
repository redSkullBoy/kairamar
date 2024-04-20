using Domain.Entities.Model;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Common;
using Domain.Entities;

namespace DataAccess.PostgreSQL;

public class AppDbContext : IdentityDbContext<AppUser>, IDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
            : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripPassenger> TripPassengers => Set<TripPassenger>();

    public async Task<(IReadOnlyCollection<T> value, int count)> PaginatedListAsync<T>(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken ctn)
    {
        var count = await source.CountAsync(ctn);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ctn);

        return (items, count);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.Id;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.Id;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
