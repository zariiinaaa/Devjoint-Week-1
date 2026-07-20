using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface IAuthorService
{
    Task<PagedResponseDto<AuthorResponseDto>> GetPagedAsync(ListQueryDto query);

    Task<AuthorResponseDto?> GetByIdAsync(int id);

    Task<AuthorResponseDto> CreateAsync(
        AuthorCreateDto dto);

    Task<bool> UpdateAsync(
        int id,
        AuthorUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}