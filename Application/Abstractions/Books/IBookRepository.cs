using Domain.Entities;
namespace Application.Abstractions.Books;

public interface IBookRepository
{
    Task AddAsync(BookEntity book, CancellationToken ct = default);
    Task<BookEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<BookEntity>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<BookEntity>> GetByAuthorIdAsync(int authorId, CancellationToken ct = default);
    void Update(BookEntity book);
    void Remove(BookEntity book);
}