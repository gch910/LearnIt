namespace LearnIt.Domain.ValueObjects;

public sealed class CourseDuration : IEquatable<CourseDuration>
{
    public int TotalMinutes { get; }
    
    public int Hours => TotalMinutes / 60;
    public int Minutes => TotalMinutes % 60;

    private CourseDuration(int totalMinutes)
    {
        TotalMinutes = totalMinutes;
    }

    public static CourseDuration FromMinutes(int totalMinutes)
    {
        if (totalMinutes <= 0)
        {
            throw new ArgumentException("Course duration must be greater than zero", nameof(totalMinutes));
        }

        return new CourseDuration(totalMinutes);
    }

    public bool Equals(CourseDuration? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return TotalMinutes == other.TotalMinutes;
    }

    public override bool Equals(object? obj)
    {
        return obj is CourseDuration other && Equals(other);
    }

    public override int GetHashCode()
    {
        return TotalMinutes.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Hours}h {Minutes}m";
    }
}