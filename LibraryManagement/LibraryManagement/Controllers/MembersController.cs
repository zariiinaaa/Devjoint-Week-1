using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberResponseDto>>> GetAll()
    {
        var members = await _memberService.GetAllAsync();

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberResponseDto>> GetById(int id)
    {
        var member = await _memberService.GetByIdAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<MemberResponseDto>> Create(
    MemberCreateDto dto)
    {
        var createdMember = await _memberService.CreateAsync(dto);

        return CreatedAtAction( nameof(GetById),new { id = createdMember.Id },
            createdMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MemberUpdateDto dto)
    {
        var updated = await _memberService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =await _memberService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}