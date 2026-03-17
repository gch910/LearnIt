namespace LearnIt.Application.Features.Courses.Queries.GetCourseById;

public sealed record GetCourseByIdResponse(
    Guid Id,
    string Title,
    string Description, 
    int DurationInMinutes,
    bool IsPublished,
    DateTime CreatedAtUtc);

