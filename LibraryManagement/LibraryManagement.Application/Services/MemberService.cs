using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<PagedResponseDto<MemberResponseDto>> GetPagedAsync(ListQueryDto query)
    {
        var (members, totalCount) =
            await _memberRepository.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.SortBy,
                query.SortDirection);

        var items = members.Select(member => new MemberResponseDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            MembershipDate = member.MembershipDate,
            IsActive = member.IsActive
        }).ToList();

        return new PagedResponseDto<MemberResponseDto>
        {
            Items = items,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)query.PageSize)
        };
    }

    public async Task<MemberResponseDto?> GetByIdAsync(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);

        if (member is null)
        {
            return null;
        }

        return MapToResponseDto(member);
    }

    public async Task<MemberResponseDto> CreateAsync(MemberCreateDto dto)
    {
        var normalizedEmail = dto.Email.Trim().ToLowerInvariant();

        var emailExists = await _memberRepository.EmailExistsAsync(normalizedEmail);

        if (emailExists)
        {
            throw new InvalidOperationException("A member with this email already exists.");
        }

        var member = new Member
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = normalizedEmail,
            PhoneNumber = dto.PhoneNumber,
            MembershipDate = DateTime.UtcNow,
            IsActive = true
        };

        var createdMember = await _memberRepository.CreateAsync(member);

        return MapToResponseDto(createdMember);
    }

    public async Task<bool> UpdateAsync(int id, MemberUpdateDto dto)
    {
        var member = await _memberRepository.GetByIdAsync(id);

        if (member is null)
        {
            return false;
        }

        var normalizedEmail = dto.Email.Trim().ToLowerInvariant();

        var emailExists = await _memberRepository.EmailExistsAsync(normalizedEmail, id);

        if (emailExists)
        {
            throw new InvalidOperationException("A member with this email already exists.");
        }

        member.FirstName = dto.FirstName;
        member.LastName = dto.LastName;
        member.Email = normalizedEmail;
        member.PhoneNumber = dto.PhoneNumber;
        member.IsActive = dto.IsActive;

        await _memberRepository.UpdateAsync(member);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);

        if (member is null)
        {
            return false;
        }

        await _memberRepository.DeleteAsync(member);

        return true;
    }

    private static MemberResponseDto MapToResponseDto(Member member)
    {
        return new MemberResponseDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            MembershipDate = member.MembershipDate,
            IsActive = member.IsActive
        };
    }
}