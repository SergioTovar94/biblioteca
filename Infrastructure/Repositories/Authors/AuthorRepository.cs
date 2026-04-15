using Application.Abstractions.Authors;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<AuthorEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.AuthorEntities.ToListAsync(cancellationToken);
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