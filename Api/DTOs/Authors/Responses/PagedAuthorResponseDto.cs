namespace Api.Dtos.Authors.Responses;

public record PagedAuthorResponseDto
{
    public IEnumerable<AuthorResponseDto> Items { get; init; } = new List<AuthorResponseDto>();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
}