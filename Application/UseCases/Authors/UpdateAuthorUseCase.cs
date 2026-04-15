using Application.Abstractions.Authors;
using Application.Abstractions.Persistence;
using Shared;
namespace Application.UseCases.Authors;

public class UpdateAuthorUseCase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateAuthorUseCase(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateAuthorCommand command, CancellationToken ct)
    {
        try
        {
            await _unitOfWork.BeginAsync(ct);
            var author = await _authorRepository.GetByIdAsync(command.Id, ct);
            if (author == null)
                return Result<bool>.Failure("Author not found");
            author.Name = command.Name;
            author.LastName = command.LastName;
            author.BirthDate = command.BirthDate;
            author.Country = command.Country;
            author.Biography = command.Biography;
            _authorRepository.Update(author);
            await _unitOfWork.CommitAsync(ct);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(ct);
            return Result<bool>.Failure($"Error updating author: {ex.Message}");
        }
    }

}