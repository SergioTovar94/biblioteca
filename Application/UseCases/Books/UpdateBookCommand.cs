namespace Application.UseCases.Books;

public record UpdateBookCommand(
    int Id,
    string Title,
    int NumberOfPages,
    DateTime PublishedDate,
    string Genre,
    int AuthorId);