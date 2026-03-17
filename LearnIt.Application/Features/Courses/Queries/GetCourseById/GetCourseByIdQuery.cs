using MediatR;

namespace LearnIt.Application.Features.Courses.Queries.GetCourseById;

public sealed record GetCourseByIdQuery(Guid Id) : IRequest<GetCourseByIdResponse?>;