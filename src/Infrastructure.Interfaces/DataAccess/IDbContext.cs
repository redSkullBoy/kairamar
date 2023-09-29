using Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces.DataAccess;

public interface IDbContext
{
    DbSet<Address> Addresses { get; }
    DbSet<Trip> Trips { get; }
    DbSet<TripPassenger> TripPassengers { get; }
    DbSet<AnotherAccount> AnotherAccounts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();
}
