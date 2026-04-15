using Domain.Entities;
using Shared;

namespace Application.Abstractions.Authors;

public interface IAuthorRepository
{
    Task<AuthorEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<AuthorEntity>> GetPagedAsync(
        int page,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken ct = default);
    Task AddAsync(AuthorEntity author, CancellationToken ct = default);
    void Update(AuthorEntity author);
    Task<bool> SoftDeleteAsync(int id, CancellationToken ct);
}