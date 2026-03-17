using LearnIt.Application.Features.Courses.Commands.CreateCourse;
using LearnIt.Application.Features.Courses.Queries.GetCourseById;
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

        group.MapGet("/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCourseByIdQuery(id), cancellationToken);

            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        return app;
    }
}