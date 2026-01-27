using Application.Abstractions;
using Application.Abstractions.Authors;
using Application.Entities;

namespace Core.Domains.Authors;

public class CreateAuthorDomain(
    IAuthorRepository repo,
    IUnitOfWork uow)
{
    public async Task<AuthorEntity> Create(AuthorEntity model, CancellationToken ct)
    {
        await uow.BeginAsync(ct);

        try
        {
            await repo.AddAuthorAsync(model, ct);
            await uow.CommitAsync(ct);
            return model;
        }
        catch
        {
            await uow.RollbackAsync(ct);
            throw;
        }
    }
}