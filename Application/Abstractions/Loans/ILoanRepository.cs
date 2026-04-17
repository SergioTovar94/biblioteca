using Domain.Entities;
namespace Application.Abstractions.Loans;

public interface ILoanRepository
{
    Task AddAsync(LoanEntity loan, CancellationToken ct = default);
    Task<LoanEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<LoanEntity>> GetAllAsync(CancellationToken ct = default);
    void Update(LoanEntity loan);
    void Remove(LoanEntity loan);
}