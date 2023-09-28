using DataAccess.Sqlite;
using Domain.Entities.Enum;

namespace Domain.Entities.Model;

public class AnotherAccount
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public AppUser? AppUser { get; set; }

    public AnotherAccountType Type { get; set; }

    public string Url { get; set; } = string.Empty;
}
