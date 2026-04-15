namespace Api.DTOs.Books.Responses;

public record BookResponseDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int NumberOfPages { get; init; }
    public DateTime PublishedDate { get; init; }
    public string Genre { get; init; } = string.Empty;
    public int AuthorId { get; init; }
    public string AuthorName { get; init; } = string.Empty; // Opcional, para mostrar nombre del autor
}