using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllAsync();

    Task<Loan?> GetByIdAsync(int id);

    Task<Loan> CreateAsync(Loan loan);

    Task UpdateAsync(Loan loan);

    Task DeleteAsync(Loan loan);
}