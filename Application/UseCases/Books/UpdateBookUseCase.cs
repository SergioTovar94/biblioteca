using Application.Abstractions.Books;
using Application.Abstractions.Persistence;
using Shared;

namespace Application.UseCases.Books;

public class UpdateBookUseCase
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateBookCommand command, CancellationToken ct)
    {
        try
        {
            await _unitOfWork.BeginAsync(ct);

            var book = await _bookRepository.GetByIdAsync(command.Id, ct);
            if (book == null)
                return Result<bool>.Failure("Book not found");

            book.Title = command.Title;
            book.NumberOfPages = command.NumberOfPages;
            book.PublishedDate = command.PublishedDate;
            book.Genre = command.Genre;
            book.AuthorId = command.AuthorId;

            _bookRepository.Update(book);
            await _unitOfWork.CommitAsync(ct);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(ct);
            return Result<bool>.Failure($"Error updating book: {ex.Message}");
        }
    }
}