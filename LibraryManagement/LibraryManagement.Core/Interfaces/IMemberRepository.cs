using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces;

public interface IMemberRepository : IBaseRepository<Member>
{
    Task<bool> EmailExistsAsync(string email,int? memberIdToExclude = null);

    Task<(IEnumerable<Member> Members, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize,
        string sortBy,
        string sortDirection);
}