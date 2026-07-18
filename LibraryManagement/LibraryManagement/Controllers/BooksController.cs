using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<BookResponseDto>>> GetAll([FromQuery] ListQueryDto query)
    {
        var result =await _bookService.GetPagedAsync(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> Create(
    BookCreateDto dto)
    {
        var createdBook =
            await _bookService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdBook.Id },
            createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      int id,
      BookUpdateDto dto)
    {
        var updated =await _bookService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}