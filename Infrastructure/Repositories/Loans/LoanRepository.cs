using Application.Abstractions.Loans;
using Domain.Entities;
using Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Loans;

public class LoansRepository(AppDbContext appDbContext) : ILoanRepository
{
    public async Task AddAsync(LoanEntity loan, CancellationToken ct)
    {
        await appDbContext.Loans.AddAsync(loan, ct);
    }

    public async Task<LoanEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await appDbContext.Loans.FindAsync(id, ct);
    }

    public async Task<IReadOnlyList<LoanEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await appDbContext.Loans
        .Include(l => l.Book)
        .ToListAsync(ct);
    }

    public void Update(LoanEntity loan)
    {
        appDbContext.Loans.Update(loan);
    }

    public void Remove(LoanEntity loan)
    {
        appDbContext.Loans.Remove(loan);
    }

}