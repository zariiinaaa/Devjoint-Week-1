using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<bool> BookCodeExistsAsync(string bookCode, int? bookIdToExclude = null);

    Task<(IEnumerable<Book> Books, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string sortBy,
        string sortDirection);
}