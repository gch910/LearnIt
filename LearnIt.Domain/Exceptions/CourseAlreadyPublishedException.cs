namespace LearnIt.Domain.Exceptions;

public sealed class CourseAlreadyPublishedException : DomainException
{
    public CourseAlreadyPublishedException(string message, string? paramName = null)
        : base (message, paramName)
    {
    }
}