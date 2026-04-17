using Application.Abstractions.Authors;
using Application.Abstractions.Books;
using Application.Abstractions.Loans;
using Application.Abstractions.Persistence;
using Infrastructure.FileStorage;
using Infrastructure.Persistences;
using Infrastructure.Queries.Authors;
using Infrastructure.Repositories.Authors;
using Infrastructure.Repositories.Books;
using Infrastructure.Repositories.Loans;
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
        services.AddScoped<ILoanRepository, LoansRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        return services;
    }
}