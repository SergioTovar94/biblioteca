using Domain.Entities;
namespace Application.Abstractions.Books;

public interface IBookRepository
{
    Task<BookEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<BookEntity>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<BookEntity>> GetByAuthorIdAsync(int authorId, CancellationToken ct = default);
    Task AddAsync(BookEntity book, CancellationToken ct = default);
    void Update(BookEntity book);
    void Remove(BookEntity book);
}