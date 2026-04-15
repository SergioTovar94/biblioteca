using Application.Abstractions.Authors;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using Shared;
namespace Infrastructure.Repositories.Authors;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task AddAsync(AuthorEntity author, CancellationToken ct)
    {
        await dbContext.AuthorEntities.AddAsync(author, ct);
    }

    public async Task<AuthorEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await dbContext.AuthorEntities.FindAsync(new object[] { id }, ct);
    }

    public async Task<PagedResult<AuthorEntity>> GetPagedAsync(
        int page,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken ct = default)
    {
        var query = dbContext.AuthorEntities
            .Where(a => !a.IsDeleted)
            .AsNoTracking();
        query = (sortBy?.ToLower()) switch
        {
            "lastname" => sortDescending ? query.OrderByDescending(a => a.LastName) : query.OrderBy(a => a.LastName),
            "birthdate" => sortDescending ? query.OrderByDescending(a => a.BirthDate) : query.OrderBy(a => a.BirthDate),
            "country" => sortDescending ? query.OrderByDescending(a => a.Country) : query.OrderBy(a => a.Country),
            _ => sortDescending ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name)
        };

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<AuthorEntity>(items, totalCount, page, pageSize);
    }

    public void Update(AuthorEntity author)
    {
        dbContext.AuthorEntities.Update(author);
    }

    public async Task<bool> SoftDeleteAsync(int id, CancellationToken ct)
    {
        var author = await dbContext.AuthorEntities
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(a => a.Id == id, ct);

        if (author == null) return false;

        bool hasBooks = await dbContext.BooksEntities.AnyAsync(b => b.AuthorId == id, ct);
        if (hasBooks)
        {
            author.IsDeleted = true;
            dbContext.AuthorEntities.Update(author);
        }
        else
        {
            dbContext.AuthorEntities.Remove(author);
        }

        return true;
    }
}