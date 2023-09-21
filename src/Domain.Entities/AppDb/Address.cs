using Domain.Entities.Common;

namespace Domain.Entities.AppDb;

public class Address : BaseAuditableEntity
{
    public int Name { get; set; }
}
