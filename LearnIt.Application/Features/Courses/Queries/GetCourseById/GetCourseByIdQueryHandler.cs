using LearnIt.Domain.Abstractions;
using MediatR;

namespace LearnIt.Application.Features.Courses.Queries.GetCourseById;

public sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, GetCourseByIdResponse?>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<GetCourseByIdResponse?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id);

        //do we need some kind of message here or how do we handle this being null?
        if (course == null)
        {
            return null;
        }

        return new GetCourseByIdResponse(
            course.Id,
            course.Title,
            course.Description,
            course.IsPublished,
            course.CreatedAtUtc
        );
    }
}