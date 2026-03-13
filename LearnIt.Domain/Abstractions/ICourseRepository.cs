using LearnIt.Domain.Entities;

namespace LearnIt.Domain.Abstractions;

public interface ICourseRepository
{
    Task AddAsync(Course course, CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}