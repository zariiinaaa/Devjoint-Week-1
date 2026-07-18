using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<Author> CreateAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Author author)
    {
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}