using MediatR;

namespace LearnIt.Application.Features.Courses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
    string Title, 
    string Description,
    int DurationInMinutes) : IRequest<Guid>;