using LearnIt.Domain.Abstractions;
using LearnIt.Domain.Entities;
using LearnIt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearnIt.Infrastructure.Repositories;

public sealed class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        await _dbContext.Courses.AddAsync(course, cancellationToken);
        //move this to unit of work
        // await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // look into strict CQRS - skip repository for direct read projections - for optimized reads
    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}