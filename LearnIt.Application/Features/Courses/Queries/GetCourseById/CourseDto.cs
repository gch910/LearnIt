namespace LearnIt.Application.Features.Courses.Queries.GetCourseById;

public sealed record CourseDto(
    Guid Id,
    string Title,
    string Description, 
    bool IsPublished,
    DateTime CreatedAtUtc);

