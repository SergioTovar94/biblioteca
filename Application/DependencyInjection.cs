using Microsoft.Extensions.DependencyInjection;
using Application.UseCases.Authors;
using Application.UseCases.Books;
using Application.UseCases.Loans;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateAuthorUseCase>();
        services.AddScoped<ListAuthorsUseCase>();
        services.AddScoped<GetAuthorUseCase>();
        services.AddScoped<UpdateAuthorUseCase>();
        services.AddScoped<DeleteAuthorUseCase>();

        services.AddScoped<CreateBookUseCase>();
        services.AddScoped<ListBooksUseCase>();
        services.AddScoped<GetBookUseCase>();
        services.AddScoped<UpdateBookUseCase>();
        services.AddScoped<DeleteBookUseCase>();

        services.AddScoped<CreateLoanUseCase>();
        services.AddScoped<GetLoanUseCase>();

        return services;
    }
}