using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();

    Task<Member?> GetByIdAsync(int id);

    Task<Member> CreateAsync(Member member);

    Task UpdateAsync(Member member);

    Task DeleteAsync(Member member);

    Task<bool> EmailExistsAsync(string email,int? memberIdToExclude = null);
}