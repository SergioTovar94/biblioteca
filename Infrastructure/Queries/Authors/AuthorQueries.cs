using Application.Abstractions.Authors;
using Application.Entities;
using Dapper;
using Infrastructure.Persistences;

namespace Infrastructure.Queries.Authors;

public class AuthorQueries(UnitOfWork unitOfWork) : IAuthorQueries
{
    public async Task<List<AuthorEntity>> GetAuthorByIdAsync(CancellationToken ct)
    {
        const string sql = @"SELECT * FROM Authorses";
        return (List<AuthorEntity>) await unitOfWork.Connection.QueryAsync<AuthorEntity>(sql, ct);
    }
}