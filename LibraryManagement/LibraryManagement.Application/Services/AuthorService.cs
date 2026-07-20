using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<PagedResponseDto<AuthorResponseDto>> GetPagedAsync(ListQueryDto query)
    {
        var (authors, totalCount) =
            await _authorRepository.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.SortBy,
                query.SortDirection);

        var items = authors.Select(author => new AuthorResponseDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography
        }).ToList();

        return new PagedResponseDto<AuthorResponseDto>
        {
            Items = items,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)query.PageSize)
        };
    }

    public async Task<AuthorResponseDto?> GetByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        if (author is null)
        {
            return null;
        }

        return MapToResponseDto(author);
    }

    public async Task<AuthorResponseDto> CreateAsync(
        AuthorCreateDto dto)
    {
        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Biography = dto.Biography
        };

        var createdAuthor =
            await _authorRepository.CreateAsync(author);

        return MapToResponseDto(createdAuthor);
    }

    public async Task<bool> UpdateAsync(
        int id,
        AuthorUpdateDto dto)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        if (author is null)
        {
            return false;
        }

        author.FirstName = dto.FirstName;
        author.LastName = dto.LastName;
        author.Biography = dto.Biography;

        await _authorRepository.UpdateAsync(author);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        if (author is null)
        {
            return false;
        }

        await _authorRepository.DeleteAsync(author);

        return true;
    }

    private static AuthorResponseDto MapToResponseDto(
        Author author)
    {
        return new AuthorResponseDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography
        };
    }
}