using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface ILoanService
{
    Task<PagedResponseDto<LoanResponseDto>> GetPagedAsync( ListQueryDto query);

    Task<LoanResponseDto?> GetByIdAsync(int id);

    Task<LoanResponseDto> CreateAsync(LoanCreateDto dto);

    Task<bool> UpdateAsync(int id,LoanUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}