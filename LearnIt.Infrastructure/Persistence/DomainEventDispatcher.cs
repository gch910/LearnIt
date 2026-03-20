using LearnIt.Application.Common;
using LearnIt.Domain.Common;
using MediatR;

namespace LearnIt.Infrastructure.Persistence;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent);

            if (notification is null)
            {
                throw new InvalidOperationException($"Could not create notification for domain event type '{domainEvent.GetType().Name}'.");
            }

            await _mediator.Publish((INotification)notification, cancellationToken);
        }
    }
}