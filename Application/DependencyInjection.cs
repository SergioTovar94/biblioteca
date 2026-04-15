using Microsoft.Extensions.DependencyInjection;
using Application.UseCases.Authors;
using Application.UseCases.Books;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateAuthorUseCase>();
        services.AddScoped<UpdateAuthorUseCase>();
        services.AddScoped<ListAuthorsUseCase>();
        services.AddScoped<GetAuthorUseCase>();

        services.AddScoped<CreateBookUseCase>();
        services.AddScoped<GetBookUseCase>();
        services.AddScoped<ListBooksUseCase>();
        services.AddScoped<UpdateBookUseCase>();
        services.AddScoped<DeleteBookUseCase>();

        return services;
    }
}