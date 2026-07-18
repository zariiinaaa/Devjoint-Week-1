using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<MemberResponseDto>> GetAllAsync();

    Task<MemberResponseDto?> GetByIdAsync(int id);

    Task<MemberResponseDto> CreateAsync(MemberCreateDto dto);

    Task<bool> UpdateAsync(int id,MemberUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}