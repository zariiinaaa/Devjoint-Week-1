using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface IMemberService
{
    Task<PagedResponseDto<MemberResponseDto>> GetPagedAsync(ListQueryDto query);

    Task<MemberResponseDto?> GetByIdAsync(int id);

    Task<MemberResponseDto> CreateAsync(MemberCreateDto dto);

    Task<bool> UpdateAsync(int id,MemberUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}