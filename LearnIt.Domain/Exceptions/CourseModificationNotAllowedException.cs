namespace LearnIt.Domain.Exceptions;

public sealed class CourseModificationNotAllowedException : DomainException
{
    public CourseModificationNotAllowedException(string message, string? paramName = null)
        : base (message, paramName)
    {
    }
}