using FluentAssertions;
using LearnIt.Domain.DomainEvents;
using LearnIt.Domain.Entities;
using LearnIt.Domain.ValueObjects;

namespace LearnIt.Domain.Tests.Entities;

public sealed class CourseTests
{
    [Fact]
    public void Create_Should_Raise_CourseCreatedDomainEvent()
    {
        // Arrange
        var duration = CourseDuration.FromMinutes(180);

        // Act 
        var course = Course.Create(
            "Test CreateCourseDomainEvent Name",
            "Test CreateCourseDomainEvent Description",
            duration
        );

        // Assert
        var domainEvent = course.DomainEvents
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeOfType<CourseCreatedDomainEvent>()
            .Which;

        domainEvent.CourseId.Should().Be(course.Id);
    }
}