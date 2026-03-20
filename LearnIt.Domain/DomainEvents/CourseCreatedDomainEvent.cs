using LearnIt.Domain.Common;

namespace LearnIt.Domain.DomainEvents;

public sealed class CourseCreatedDomainEvent : DomainEvent
{
    public Guid CourseId { get; }

    public CourseCreatedDomainEvent(Guid courseId)
    {
        CourseId = courseId;
    }
}