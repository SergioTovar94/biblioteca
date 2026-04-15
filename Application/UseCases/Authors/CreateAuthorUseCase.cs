using Application.Abstractions.Authors;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Authors;

public class CreateAuthorUseCase
{
    private readonly IAuthorRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateAuthorUseCase(
        IAuthorRepository repo,
        IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<int>> Execute(
        CreateAuthorCommand command,
        CancellationToken ct)
    {
        await _uow.BeginAsync(ct);

        try
        {
            var author = new AuthorEntity
            {
                Name = command.Name,
                LastName = command.LastName,
                BirthDate = command.BirthDate,
                Country = command.Country,
                Biography = command.Biography,
                IsDeleted = false,
                Books = new List<BookEntity>()
            };

            await _repo.AddAsync(author, ct);

            await _uow.CommitAsync(ct);

            return Result<int>.Success(author.Id);
        }
        catch (Exception ex)
        {
            await _uow.RollbackAsync(ct);
            return Result<int>.Failure($"Error creating author: {ex.Message}");
        }
    }
}