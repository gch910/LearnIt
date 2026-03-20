using LearnIt.Domain.Common;
using MediatR;

namespace LearnIt.Application.Common;

// wraps a domain event as a MediatR INotification so it can be published and handled via mediator pipeline
public sealed class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}