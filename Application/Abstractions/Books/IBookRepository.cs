using Domain.Entities;
namespace Application.Abstractions.Books;

public interface IBookRepository
{
    Task<BookEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<BookEntity>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);
    Task AddAsync(BookEntity book, CancellationToken ct = default);
    void Update(BookEntity book);
    void Remove(BookEntity book);
}