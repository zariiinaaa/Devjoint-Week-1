using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<(IEnumerable<Author> Authors, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string sortBy,
        string sortDirection);
}