using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface ILoanRepository : IBaseRepository<Loan>
{
    Task<(IEnumerable<Loan> Loans, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string sortBy,
        string sortDirection);
}