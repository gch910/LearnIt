using LearnIt.Application.Common;
using LearnIt.Domain.Abstractions;
using LearnIt.Domain.Common;
using LearnIt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnIt.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventDispatcher domainEventDispatcher) 
        : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public DbSet<Course> Courses => Set<Course>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.DomainEvents.Any())
            .SelectMany(entity => entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var entity in ChangeTracker.Entries<Entity>().Select(entry => entry.Entity))
        {
            entity.ClearDomainEvents();
        }

        await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}