using Domain.Entities.Common;

namespace Domain.Entities.Models;

public class Address : BaseAuditableEntity
{
    public int Name { get; set; }
}
