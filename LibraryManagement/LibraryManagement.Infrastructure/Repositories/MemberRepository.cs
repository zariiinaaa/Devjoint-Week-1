using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class MemberRepository: BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<bool> EmailExistsAsync(
        string email,
        int? memberIdToExclude = null)
    {
        return await _context.Members.AnyAsync(member =>
            member.Email == email &&
            (!memberIdToExclude.HasValue ||
             member.Id != memberIdToExclude.Value));
    }

    public async Task<(IEnumerable<Member> Members, int TotalCount)>
    GetPagedAsync(int pageNumber,int pageSize,
        string sortBy,
        string sortDirection)
    {
        var query = _context.Members.AsNoTracking();

        var totalCount = await query.CountAsync();

        var descending = sortDirection.Equals(
            "desc",
            StringComparison.OrdinalIgnoreCase);

        query = sortBy.ToLower() switch
        {
            "firstname" => descending
                ? query.OrderByDescending(member => member.FirstName)
                : query.OrderBy(member => member.FirstName),

            "lastname" => descending
                ? query.OrderByDescending(member => member.LastName)
                : query.OrderBy(member => member.LastName),

            "email" => descending
                ? query.OrderByDescending(member => member.Email)
                : query.OrderBy(member => member.Email),

            "membershipdate" => descending
                ? query.OrderByDescending(member => member.MembershipDate)
                : query.OrderBy(member => member.MembershipDate),

            "isactive" => descending
                ? query.OrderByDescending(member => member.IsActive)
                : query.OrderBy(member => member.IsActive),

            _ => descending
                ? query.OrderByDescending(member => member.Id)
                : query.OrderBy(member => member.Id)
        };

        var members = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (members, totalCount);
    }
}