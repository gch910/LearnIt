using LearnIt.Domain.Entities;
using LearnIt.Domain.Abstractions;
using MediatR;

namespace LearnIt.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(request.Title, request.Description);

        await _courseRepository.AddAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}