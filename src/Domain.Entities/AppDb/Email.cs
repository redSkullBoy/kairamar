using Domain.Entities.Common;

namespace Domain.Entities.AppDb;

public class Email : BaseAuditableEntity
{
    public string? Address { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public bool IsSended { get; set; }
    public int Attempts { get; set; }
}
