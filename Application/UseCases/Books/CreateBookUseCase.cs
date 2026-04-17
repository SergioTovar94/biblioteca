using Application.Abstractions.Books;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Books;

public class CreateBookUseCase
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateBookCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            return Result<int>.Failure("Title is required");

        try
        {
            await _unitOfWork.BeginAsync(ct);

            var book = new BookEntity
            {
                Title = command.Title,
                NumberOfPages = command.NumberOfPages,
                PublishedDate = command.PublishedDate,
                Genre = command.Genre,
                AuthorId = command.AuthorId,
                CoverImagePath = command.CoverImagePath
            };

            await _bookRepository.AddAsync(book, ct);
            await _unitOfWork.CommitAsync(ct);

            return Result<int>.Success(book.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(ct);
            return Result<int>.Failure($"Error creating book: {ex.Message}");
        }
    }
}