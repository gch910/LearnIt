using LearnIt.Domain.Common;
using LearnIt.Domain.DomainEvents;
using LearnIt.Domain.Exceptions;
using LearnIt.Domain.ValueObjects;

namespace LearnIt.Domain.Entities;

public sealed class Course : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public CourseDuration Duration { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    //EF core
    private Course()
    {
        Title = string.Empty;
        Description = string.Empty;
        Duration = CourseDuration.FromMinutes(1);
    }

    private Course(Guid id, string title, string description, CourseDuration duration)
    {
        Id = id;
        Title = title;
        Description = description;
        Duration = duration;
        IsPublished = false;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public static Course Create(string title, string description, CourseDuration duration)
    {
        ValidateTitle(title);
        ValidateDescription(description);

        var course = new Course(Guid.NewGuid(), title.Trim(), description.Trim(), duration);

        course.AddDomainEvent(new CourseCreatedDomainEvent(course.Id));

        return course;
    }

    public void UpdateDetails(string title, string description, CourseDuration duration)
    {
        EnsureNotPublished();

        ValidateTitle(title);
        ValidateDescription(description);

        Title = title.Trim();
        Description = description.Trim();
        Duration = duration;
    }

    public void Publish()
    {
        if (IsPublished)
        {
            throw new CourseAlreadyPublishedException("Course is already published.");
        }

        IsPublished = true;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidCourseTitleException("Course title is required", nameof(title));
        }

        if (title.Trim().Length > 200)
        {
            throw new InvalidCourseTitleException("Course title cannot exceed 200 characters.", nameof(title));
        }
    }

    private static void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new InvalidCourseDescriptionException($"Course description is required.", nameof(description));
        }

        if (description.Trim().Length > 2000)
        {
            throw new InvalidCourseDescriptionException("Course description cannot exceed 2000 characters.", nameof(description));
        }
    }

    private void EnsureNotPublished()
    {
        if (IsPublished)
        {
            throw new CourseModificationNotAllowedException("Published courses cannot be modified.");
        }
    }
}