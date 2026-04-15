using Application.Abstractions.Books;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Books;

public class GetBookUseCase
{
    private readonly IBookRepository _bookRepository;

    public GetBookUseCase(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<BookEntity>> Execute(GetBookQuery query, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(query.Id, cancellationToken);
        if (book == null)
            return Result<BookEntity>.Failure("Book not found");

        return Result<BookEntity>.Success(book);
    }
}