using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IBookRepository
{
    

    Task<(IEnumerable<Book> Books, int TotalCount)> GetPagedAsync(int pageNumber,int pageSize,
    string sortBy,
    string sortDirection);

    Task<Book?> GetByIdAsync(int id);

    Task<Book> CreateAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(Book book);

    Task<bool> BookCodeExistsAsync(
        string bookCode,
        int? bookIdToExclude = null);
}