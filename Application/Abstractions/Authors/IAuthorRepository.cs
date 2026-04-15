using Domain.Entities;

namespace Application.Abstractions.Authors;

public interface IAuthorRepository
{
    Task<AuthorEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AuthorEntity author, CancellationToken ct = default);
    void Update(AuthorEntity author);
    void Remove(AuthorEntity author);
}