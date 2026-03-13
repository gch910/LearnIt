namespace LearnIt.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public string? ParamName { get; }
    protected DomainException(string message, string? paramName = null) : base(message)
    {
        ParamName = paramName;
    }
    
    // can later catch domain exceptions in API and translate them into HTTP responses here.
}