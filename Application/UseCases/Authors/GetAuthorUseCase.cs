using Application.Abstractions.Authors;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Authors;

public class GetAuthorUseCase
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorUseCase(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Result<AuthorEntity>> Handler(GetAuthorQuery query, CancellationToken ct)
    {
        var author = await _authorRepository.GetByIdAsync(query.Id, ct);
        if (author == null)
            return Result<AuthorEntity>.Failure("Author not found");
        return Result<AuthorEntity>.Success(author);
    }

}