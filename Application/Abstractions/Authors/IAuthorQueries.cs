using Domain.Entities;

namespace Application.Abstractions.Authors;

public interface IAuthorQueries
{
    // Task<List<AuthorEntity>> GetAuthorByIdAsync(CancellationToken ct);
    Task<List<AuthorEntity>> GetAllAsync(CancellationToken ct);

}