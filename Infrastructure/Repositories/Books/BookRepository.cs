using Application.Abstractions.Books;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Books;

public class BookRepository(AppDbContext appDbContext) : IBookRepository
{
    public async Task AddAsync(BookEntity book, CancellationToken ct)
    {
        await appDbContext.Books.AddAsync(book, ct);
    }
    public async Task<BookEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await appDbContext.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id, ct);
    }

    public async Task<IReadOnlyList<BookEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await appDbContext.Books
            .Include(b => b.Author)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<BookEntity>> GetByAuthorIdAsync(int authorId, CancellationToken ct = default)
    {
        return await appDbContext.Books
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.Author)
            .ToListAsync(ct);
    }

    public void Update(BookEntity book)
    {
        appDbContext.Books.Update(book);
    }


    public void Remove(BookEntity book)
    {
        appDbContext.Books.Remove(book);
    }

}