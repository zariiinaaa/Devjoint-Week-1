using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository
    : BaseRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context)
        : base(context) { }

    public async Task<bool> BookCodeExistsAsync(string bookCode,int? bookIdToExclude = null)
    {
        return await _context.Books.AnyAsync(book =>
            book.BookCode == bookCode &&(!bookIdToExclude.HasValue ||
             book.Id != bookIdToExclude.Value));
    }

    public async Task<(IEnumerable<Book> Books, int TotalCount)>
        GetPagedAsync(
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortDirection)
    {
        var query = _context.Books.AsNoTracking();

        var totalCount = await query.CountAsync();

        var descending = sortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = sortBy.ToLower() switch
        {
            "id" => descending
                ? query.OrderByDescending(book => book.Id)
                : query.OrderBy(book => book.Id),
            "bookcode" => descending
                ? query.OrderByDescending(book => book.BookCode)
                : query.OrderBy(book => book.BookCode),

            "publishedyear" => descending
                ? query.OrderByDescending(book => book.PublishedYear)
                : query.OrderBy(book => book.PublishedYear),

            _ => descending
                ? query.OrderByDescending(book => book.Title)
                : query.OrderBy(book => book.Title)
        };

        var books = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (books, totalCount);
    }
}