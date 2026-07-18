using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books.Select(book => MapToResponseDto(book));
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return null;
        }

        return MapToResponseDto(book);
    }

    public async Task<BookResponseDto> CreateAsync(BookCreateDto dto)
    {
        ValidateCopies(dto.TotalCopies, dto.AvailableCopies);

        var bookCodeExists =
            await _bookRepository.BookCodeExistsAsync(dto.BookCode);

        if (bookCodeExists)
        {
            throw new InvalidOperationException(
                "A book with this code already exists.");
        }

        var book = new Book
        {
            Title = dto.Title,
            BookCode = dto.BookCode,
            PublishedYear = dto.PublishedYear,
            TotalCopies = dto.TotalCopies,
            AvailableCopies = dto.AvailableCopies
        };

        var createdBook = await _bookRepository.CreateAsync(book);

        return MapToResponseDto(createdBook);
    }

    public async Task<bool> UpdateAsync(int id,BookUpdateDto dto)
    {
        ValidateCopies(dto.TotalCopies, dto.AvailableCopies);

        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return false;
        }

        var bookCodeExists =await _bookRepository.BookCodeExistsAsync(dto.BookCode,id);

        if (bookCodeExists)
        {
            throw new InvalidOperationException(
                "A book with this code already exists.");
        }

        book.Title = dto.Title;
        book.BookCode = dto.BookCode;
        book.PublishedYear = dto.PublishedYear;
        book.TotalCopies = dto.TotalCopies;
        book.AvailableCopies = dto.AvailableCopies;

        await _bookRepository.UpdateAsync(book);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return false;
        }

        await _bookRepository.DeleteAsync(book);

        return true;
    }

    private static BookResponseDto MapToResponseDto(Book book)
    {
        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            BookCode = book.BookCode,
            PublishedYear = book.PublishedYear,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies
        };
    }

    private static void ValidateCopies(int totalCopies,int availableCopies)
    {
        if (availableCopies > totalCopies)
        {
            throw new ArgumentException(
                "Available copies cannot exceed total copies.");
        }
    }
}