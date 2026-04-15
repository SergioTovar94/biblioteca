namespace Api.Dtos.Authors.Responses;

public class AuthorResponseDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime? BirthDate { get; init; }
    public string? Country { get; init; } = string.Empty;
    public string? Biography { get; init; } = string.Empty;
}