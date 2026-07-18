using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();

    Task<Author?> GetByIdAsync(int id);

    Task<Author> CreateAsync(Author author);

    Task UpdateAsync(Author author);

    Task DeleteAsync(Author author);
}