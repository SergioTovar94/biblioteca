using Application.Abstractions.Authors;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Authors;

public class ListAuthorsUseCase
{
    private readonly IAuthorRepository _authorRepository;

    public ListAuthorsUseCase(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Result<PagedResult<AuthorEntity>>> Handle(ListAuthorsQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _authorRepository.GetPagedAsync(
            query.Page,
            query.PageSize,
            query.SortBy,
            query.SortDescending,
            cancellationToken);

        return Result<PagedResult<AuthorEntity>>.Success(pagedResult);
    }
}
