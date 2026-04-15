using Application.Abstractions.Books;
using Application.Abstractions.Persistence;
using Shared;

namespace Application.UseCases.Books;

public class DeleteBookUseCase
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginAsync(cancellationToken);

            var book = await _bookRepository.GetByIdAsync(command.Id, cancellationToken);
            if (book == null)
                return Result<bool>.Failure("Book not found");

            _bookRepository.Remove(book);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            return Result<bool>.Failure($"Error deleting book: {ex.Message}");
        }
    }
}