using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetAll()
    {
        var loans = await _loanService.GetAllAsync();

        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanResponseDto>> GetById(int id)
    {
        var loan = await _loanService.GetByIdAsync(id);

        if (loan is null)
        {
            return NotFound();
        }

        return Ok(loan);
    }

    [HttpPost]
    public async Task<ActionResult<LoanResponseDto>> Create(LoanCreateDto dto)
    {
        var createdLoan =await _loanService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = createdLoan.Id },
            createdLoan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    LoanUpdateDto dto)
    {
        var updated =await _loanService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _loanService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}