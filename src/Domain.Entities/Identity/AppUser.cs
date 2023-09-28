using Domain.Entities.Enum;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace DataAccess.Sqlite;

public class AppUser : IdentityUser
{
    public AppUserType Type { get; set; }

    public Collection<AnotherAccount>? AnotherAccounts { get; set; }
}
