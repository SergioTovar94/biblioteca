using Application.Abstractions.Authors;
using Application.Entities;

namespace Core.Domains.Authors;

public class AuthorListDomain(IAuthorQueries authorQueries)
{
    public async Task<List<AuthorEntity>> GetAuthorsAsync(CancellationToken ct) => await authorQueries.GetAllAsync(ct);

}