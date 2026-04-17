namespace Api.Dtos.Loans.Responses;

public record LoanResponseDto
{
    public int Id { get; init; }
    public int BookId { get; init; }
    public string BorrowerName { get; init; } = string.Empty;
    public DateTime LoanDate { get; init; }
    public DateTime DueDate { get; init; }
}