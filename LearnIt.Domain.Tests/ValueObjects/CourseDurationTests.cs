using FluentAssertions;
using LearnIt.Domain.ValueObjects;

namespace LearnIt.Domain.Tests.ValueObjects;

public sealed class CourseDurationTests
{
    [Fact]
    public void FromMinutes_Should_Create_Duration_When_Value_Is_Valid()
    {
        var duration = CourseDuration.FromMinutes(180);

        duration.TotalMinutes.Should().Be(180);
        duration.Hours.Should().Be(3);
        duration.Minutes.Should().Be(0);
    }

    [Fact]
    public void FromMinutes_Should_Throw_When_Value_Is_Zero_Or_Less()
    {
        var act = () => CourseDuration.FromMinutes(0);

        act.Should().Throw<ArgumentException>();
    }
}