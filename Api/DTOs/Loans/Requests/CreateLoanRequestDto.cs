using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Loans.Requests;

public record CreateLoanRequestDto
{
    [Range(1, int.MaxValue)]
    public int BookId { get; init; }

    [Required]
    [MaxLength(200)]
    public string BorrowerName { get; init; } = string.Empty;

    public DateTime LoanDate { get; init; }

    public DateTime DueDate { get; init; }
}