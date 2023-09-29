using Domain.Entities.Common;

namespace Infrastructure.Interfaces.Services;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}