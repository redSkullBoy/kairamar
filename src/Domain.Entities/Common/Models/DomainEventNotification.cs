//using MediatR;

namespace Domain.Entities.Common.Models;

public class DomainEventNotification<TDomainEvent>// : INotification where TDomainEvent : DomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}