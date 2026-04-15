using Application.Abstractions.Books;
using Application.UseCases.Authors;
using Application.UseCases.Books;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories.Books;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Biblioteca API",
        Version = "v1"
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CreateAuthorUseCase>();
builder.Services.AddScoped<ListAuthorsUseCase>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<CreateBookUseCase>();
builder.Services.AddScoped<GetBookUseCase>();
builder.Services.AddScoped<ListBooksUseCase>();
builder.Services.AddScoped<UpdateBookUseCase>();
builder.Services.AddScoped<DeleteBookUseCase>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();