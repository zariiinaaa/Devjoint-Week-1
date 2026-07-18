using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface IBookService
{
   
    Task<PagedResponseDto<BookResponseDto>> GetPagedAsync(ListQueryDto query);
    Task<BookResponseDto?> GetByIdAsync(int id);

    Task<BookResponseDto> CreateAsync(BookCreateDto dto);

    Task<bool> UpdateAsync(
        int id,
        BookUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}