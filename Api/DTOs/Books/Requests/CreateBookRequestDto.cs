namespace Api.DTOs.Books.Requests;

public record CreateBookRequestDto
{
    public string Title { get; init; } = string.Empty;
    public int NumberOfPages { get; init; }
    public DateTime PublishedDate { get; init; }
    public string Genre { get; init; } = string.Empty;
    public int AuthorId { get; init; }
}