using Domain.Entities.Enum;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? TimeZoneId { get; set; }

    public AppUserType UserType { get; set; }

    public int? LastAddressId { get; set; }
    public Address? LastAddress { get; set; } = null;
}
