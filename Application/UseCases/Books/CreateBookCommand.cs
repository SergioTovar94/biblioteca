namespace Application.UseCases.Books;

public record CreateBookCommand(
    string Title,
    int NumberOfPages,
    DateTime PublishedDate,
    string Genre,
    int AuthorId,
    string CoverImagePath);