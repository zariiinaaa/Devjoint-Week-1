using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _context.Members
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.AsNoTracking().FirstOrDefaultAsync(member => member.Id == id);
    }

    public async Task<Member> CreateAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Member member)
    {
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email,int? memberIdToExclude = null)
    {
        if (memberIdToExclude.HasValue)
        {
            return await _context.Members.AnyAsync(member =>
                member.Email == email &&
                member.Id != memberIdToExclude.Value);
        }

        return await _context.Members.AnyAsync(member =>member.Email == email);
    }
}