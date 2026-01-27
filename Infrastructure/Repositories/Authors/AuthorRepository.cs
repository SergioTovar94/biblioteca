using Application.Abstractions.Authors;
using Application.Entities;
using Infrastructure.Persistences;

namespace Infrastructure.Repositories.Authors;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task AddAuthorAsync(AuthorEntity author, CancellationToken ct)
    {
        await dbContext.AuthorEntities.AddAsync(author, ct);
    }
}