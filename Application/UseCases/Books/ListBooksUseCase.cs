using Application.Abstractions.Books;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Books;

public class ListBooksUseCase
{
    private readonly IBookRepository _bookRepository;

    public ListBooksUseCase(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<IEnumerable<BookEntity>>> Handle(ListBooksQuery _, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);
        return Result<IEnumerable<BookEntity>>.Success(books);
    }
}