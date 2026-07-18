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

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync();
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