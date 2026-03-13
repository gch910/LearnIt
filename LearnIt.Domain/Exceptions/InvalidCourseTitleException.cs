namespace LearnIt.Domain.Exceptions;

public sealed class InvalidCourseTitleException : DomainException
{
    public InvalidCourseTitleException(string message, string? paramName = null)
        : base (message, paramName)
    {
    }
}