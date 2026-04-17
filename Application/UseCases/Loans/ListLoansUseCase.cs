using Application.Abstractions.Loans;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Loans;

public class ListLoansUseCase
{
    private readonly ILoanRepository _loanRepository;

    public ListLoansUseCase(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<Result<IEnumerable<LoanEntity>>> Handle(ListLoansQuery _, CancellationToken ct)
    {
        var loans = await _loanRepository.GetAllAsync(ct);
        return Result<IEnumerable<LoanEntity>>.Success(loans);
    }
}