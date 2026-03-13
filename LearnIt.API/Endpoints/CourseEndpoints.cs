using LearnIt.Application.Features.Courses.Commands.CreateCourse;
using MediatR;

namespace LearnIt.API.Endpoints;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCoursesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/courses");

        group.MapPost("/", async (
            CreateCourseCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var courseId = await sender.Send(command, cancellationToken);

            return Results.Created($"/api/courses/{courseId}", new { id = courseId });
        });

        return app;
    }
}