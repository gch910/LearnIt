using LearnIt.Domain.Abstractions;
using LearnIt.Infrastructure.Persistence;
using LearnIt.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnIt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LearnItDatabase");

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlite(connectionString));

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}