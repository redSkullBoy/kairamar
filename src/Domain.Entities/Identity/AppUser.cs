using Domain.Entities.Enum;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace DataAccess.Sqlite;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public AppUserType UserType { get; set; }

    public Collection<AnotherAccount>? AnotherAccounts { get; set; }
}
