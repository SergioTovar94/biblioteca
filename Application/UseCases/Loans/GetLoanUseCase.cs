using Application.Abstractions.Loans;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Loans;

public class GetLoanUseCase
{
    private readonly ILoanRepository _loanRepository;

    public GetLoanUseCase(
        ILoanRepository loanRepository
        )
    {
        _loanRepository = loanRepository;
    }

    public async Task<Result<LoanEntity>> Handler(GetLoanQuery query, CancellationToken ct)
    {
        var loan = await _loanRepository.GetByIdAsync(query.Id, ct);
        if (loan == null)
            return Result<LoanEntity>.Failure("Loan Not Found");
        return Result<LoanEntity>.Success(loan);
    }

}