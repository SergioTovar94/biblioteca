using Application.Abstractions.Authors;
using Application.Abstractions.Persistence;
using Shared;

namespace Application.UseCases.Authors;

public class DeleteAuthorUseCase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAuthorUseCase(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteAuthorCommand command, CancellationToken ct)
    {
        try
        {
            await _unitOfWork.BeginAsync(ct);
            var author = await _authorRepository.GetByIdAsync(command.Id, ct);
            if (author == null)
                return Result<bool>.Failure("Author not found");
            _authorRepository.Remove(author);
            await _unitOfWork.CommitAsync(ct);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(ct);
            return Result<bool>.Failure($"Error deleting book: {ex.Message}");
        }
    }

}