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

    public async Task<Result<bool>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginAsync(cancellationToken);

            var book = await _bookRepository.GetByIdAsync(command.Id, cancellationToken);
            if (book == null)
                return Result<bool>.Failure("Book not found");

            book.Title = command.Title;
            book.NumberOfPages = command.NumberOfPages;
            book.PublishedDate = command.PublishedDate;
            book.Genre = command.Genre;
            book.AuthorId = command.AuthorId;

            _bookRepository.Update(book);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            return Result<bool>.Failure($"Error updating book: {ex.Message}");
        }
    }
}