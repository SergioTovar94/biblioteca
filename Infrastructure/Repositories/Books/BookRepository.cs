using Application.Abstractions.Books;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Books;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;

    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbContext.BooksEntities
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IEnumerable<BookEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbContext.BooksEntities
            .Include(b => b.Author)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<BookEntity>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default)
        => await _dbContext.BooksEntities
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.Author)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(BookEntity book, CancellationToken ct = default)
        => await _dbContext.BooksEntities.AddAsync(book, ct);

    public void Update(BookEntity book) => _dbContext.BooksEntities.Update(book);

    public void Remove(BookEntity book) => _dbContext.BooksEntities.Remove(book);
}