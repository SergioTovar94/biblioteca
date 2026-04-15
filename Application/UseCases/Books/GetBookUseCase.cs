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

    public async Task<Result<BookEntity>> Handle(GetBookQuery query, CancellationToken ct)
    {
        var book = await _bookRepository.GetByIdAsync(query.Id, ct);
        if (book == null)
            return Result<BookEntity>.Failure("Book not found");

        return Result<BookEntity>.Success(book);
    }
}