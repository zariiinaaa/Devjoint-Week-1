using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAll()
    {
        var authors = await _authorService.GetAllAsync();

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponseDto>> GetById(int id)
    {
        var author = await _authorService.GetByIdAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponseDto>> Create(
        AuthorCreateDto dto)
    {
        var createdAuthor =
            await _authorService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdAuthor.Id },
            createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        AuthorUpdateDto dto)
    {
        var updated =
            await _authorService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _authorService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}