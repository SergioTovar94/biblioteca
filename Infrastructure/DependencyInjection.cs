using Application.Abstractions.Authors;
using Application.Abstractions.Books;
using Application.Abstractions.Persistence;
using Infrastructure.FileStorage;
using Infrastructure.Persistences;
using Infrastructure.Queries.Authors;
using Infrastructure.Repositories.Authors;
using Infrastructure.Repositories.Books;
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
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        return services;
    }
}