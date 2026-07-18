using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

   
    public async Task<(IEnumerable<Book> Books, int TotalCount)> GetPagedAsync(int pageNumber,int pageSize,
    string sortBy,
    string sortDirection)
    {
        var query = _context.Books
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var isAscending = sortDirection.Equals(
            "asc",
            StringComparison.OrdinalIgnoreCase);

        var isDescending = sortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        if (!isAscending && !isDescending)
        {
            throw new ArgumentException(
                "Sort direction must be 'asc' or 'desc'.");
        }

        var orderedQuery = sortBy.ToLowerInvariant() switch
        {
            "title" => isDescending
                ? query.OrderByDescending(book => book.Title)
                : query.OrderBy(book => book.Title),

            "bookcode" => isDescending
                ? query.OrderByDescending(book => book.BookCode)
                : query.OrderBy(book => book.BookCode),

            "publishedyear" => isDescending
                ? query.OrderByDescending(book => book.PublishedYear)
                : query.OrderBy(book => book.PublishedYear),

            _ => throw new ArgumentException(
                "Sort by must be 'title', 'bookCode' or 'publishedYear'.")
        };

        var books = await orderedQuery.Skip((pageNumber - 1) * pageSize) .Take(pageSize)
            .ToListAsync();

        return (books, totalCount);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> BookCodeExistsAsync(
        string bookCode,
        int? bookIdToExclude = null)
    {
        if (bookIdToExclude.HasValue)
        {
            return await _context.Books.AnyAsync(book =>
                book.BookCode == bookCode &&
                book.Id != bookIdToExclude.Value);
        }

        return await _context.Books.AnyAsync(book =>
            book.BookCode == bookCode);
    }
}