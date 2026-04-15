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

    public async Task<AuthorEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.AuthorEntities.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<PagedResult<AuthorEntity>> GetPagedAsync(
        int page,
        int pageSize,
        string sortBy,
        bool sortDescending,
        CancellationToken cancellationToken = default)
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

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<AuthorEntity>(items, totalCount, page, pageSize);
    }

    public void Update(AuthorEntity author)
    {
        dbContext.AuthorEntities.Update(author);
    }

    public void Remove(AuthorEntity author)
    {
        dbContext.AuthorEntities.Remove(author);
    }
}