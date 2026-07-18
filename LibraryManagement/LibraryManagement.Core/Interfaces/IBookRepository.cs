using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<Book> CreateAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(Book book);

    Task<bool> BookCodeExistsAsync(
        string bookCode,
        int? bookIdToExclude = null);
}