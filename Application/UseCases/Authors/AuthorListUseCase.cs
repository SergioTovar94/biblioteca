using Application.Abstractions.Authors;
using Domain.Entities;

namespace Application.UseCases.Authors;

public class AuthorListUseCase(IAuthorQueries authorQueries)
{
    public async Task<List<AuthorEntity>> GetAuthorsAsync(CancellationToken ct) => await authorQueries.GetAllAsync(ct);

}