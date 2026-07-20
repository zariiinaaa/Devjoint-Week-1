using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class LoanRepository: BaseRepository<Loan>, ILoanRepository
{
    public LoanRepository(AppDbContext context): base(context) { }

    public async Task<(IEnumerable<Loan> Loans, int TotalCount)>
        GetPagedAsync(
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortDirection)
    {
        var query = _context.Loans.AsNoTracking();

        var totalCount = await query.CountAsync();

        var descending = sortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = sortBy.ToLower() switch
        {
            "bookid" => descending
                ? query.OrderByDescending(loan => loan.BookId)
                : query.OrderBy(loan => loan.BookId),

            "memberid" => descending
                ? query.OrderByDescending(loan => loan.MemberId)
                : query.OrderBy(loan => loan.MemberId),

            "borrowedat" => descending
                ? query.OrderByDescending(loan => loan.BorrowedAt)
                : query.OrderBy(loan => loan.BorrowedAt),

            "duedate" => descending
                ? query.OrderByDescending(loan => loan.DueDate)
                : query.OrderBy(loan => loan.DueDate),

            "returnedat" => descending
                ? query.OrderByDescending(loan => loan.ReturnedAt)
                : query.OrderBy(loan => loan.ReturnedAt),

            _ => descending
                ? query.OrderByDescending(loan => loan.Id)
                : query.OrderBy(loan => loan.Id)
        };

        var loans = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (loans, totalCount);
    }
}