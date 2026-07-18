using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core.Interfaces;

public interface ILoanService
{
    Task<IEnumerable<LoanResponseDto>> GetAllAsync();

    Task<LoanResponseDto?> GetByIdAsync(int id);

    Task<LoanResponseDto> CreateAsync(LoanCreateDto dto);

    Task<bool> UpdateAsync(int id,LoanUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}