using Application.Abstractions;
using Application.Abstractions.Authors;
using Infrastructure.Persistences;
using Infrastructure.Queries.Authors;
using Infrastructure.Repositories.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default")));

        services.AddScoped<UnitOfWork>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UnitOfWork>());

        services.AddScoped<IAuthorQueries, AuthorQueries>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        return services;
    }
}