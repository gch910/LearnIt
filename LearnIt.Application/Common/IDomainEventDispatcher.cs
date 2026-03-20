using LearnIt.Domain.Common;

namespace LearnIt.Application.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}