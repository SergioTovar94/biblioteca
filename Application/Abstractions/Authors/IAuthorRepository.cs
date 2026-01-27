using Application.Entities;

namespace Application.Abstractions.Authors;

public interface IAuthorRepository
{
    Task AddAuthorAsync(AuthorEntity author, CancellationToken ct);
}