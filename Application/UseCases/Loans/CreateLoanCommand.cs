using Domain.Entities;

namespace Application.UseCases.Loans;

public record CreateLoanCommand(
     int BookId,
     string BorrowerName,
     DateTime LoanDate,
     DateTime DueDate
);