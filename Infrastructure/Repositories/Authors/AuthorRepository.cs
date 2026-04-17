using Application.Abstractions.Authors;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using Shared;
namespace Infrastructure.Repositories.Authors;

public class AuthorRepository(AppDbContext appDbContext) : IAuthorRepository
{
    public async Task AddAsync(AuthorEntity author, CancellationToken ct)
    {
        await appDbContext.Authors.AddAsync(author, ct);
    }

    public async Task<AuthorEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await appDbContext.Authors.FindAsync(id, ct);
    }

    public async Task<PagedResult<AuthorEntity>> GetPagedAsync(
        int page,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken ct = default)
    {
        var query = appDbContext.Authors
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
        appDbContext.Authors.Update(author);
    }

    public async Task<bool> SoftDeleteAsync(int id, CancellationToken ct)
    {
        var author = await appDbContext.Authors
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(a => a.Id == id, ct);

        if (author == null) return false;

        bool hasBooks = await appDbContext.Books.AnyAsync(b => b.AuthorId == id, ct);
        if (hasBooks)
        {
            author.IsDeleted = true;
            appDbContext.Authors.Update(author);
        }
        else
        {
            appDbContext.Authors.Remove(author);
        }

        return true;
    }
}