using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class AuthorRepository: BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(AppDbContext context): base(context) { }

    public async Task<(IEnumerable<Author> Authors, int TotalCount)>
        GetPagedAsync(
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortDirection)
    {
        var query = _context.Authors.AsNoTracking();

        var totalCount = await query.CountAsync();

        var descending = sortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = sortBy.ToLower() switch
        {
            "firstname" => descending
                ? query.OrderByDescending(author => author.FirstName)
                : query.OrderBy(author => author.FirstName),

            "lastname" => descending
                ? query.OrderByDescending(author => author.LastName)
                : query.OrderBy(author => author.LastName),

            _ => descending
                ? query.OrderByDescending(author => author.Id)
                : query.OrderBy(author => author.Id)
        };

        var authors = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (authors, totalCount);
    }
}