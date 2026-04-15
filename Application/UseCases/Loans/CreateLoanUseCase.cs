using Application.Abstractions.Loans;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Shared;

namespace Application.UseCases.Loans;

public class CreateLoanUseCase
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanUseCase(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateLoanCommand command, CancellationToken ct)
    {
        try
        {
            await _unitOfWork.BeginAsync(ct);
            var loan = new LoanEntity
            {

                BookId = command.BookId,
                BorrowerName = command.BorrowerName,
                LoanDate = command.LoanDate,
                DueDate = command.DueDate
            };
            await _loanRepository.AddAsync(loan, ct);
            await _unitOfWork.CommitAsync(ct);
            return Result<int>.Success(loan.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(ct);
            return Result<int>.Failure($"Error creating loan: {ex.Message}");
        }
    }

}