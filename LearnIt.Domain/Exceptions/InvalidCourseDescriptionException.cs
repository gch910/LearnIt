namespace LearnIt.Domain.Exceptions;

public sealed class InvalidCourseDescriptionException : DomainException
{
    public InvalidCourseDescriptionException(string message, string? paramName = null)
        : base(message, paramName)
    {
    }
}