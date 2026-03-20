namespace LearnIt.Domain.Common;

//this can optionally implement INotification but will create mediatR dependency 
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}