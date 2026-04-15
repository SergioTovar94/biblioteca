namespace Api.Dtos.Authors.Responses;

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}