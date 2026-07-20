using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;

    public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository,
        IMemberRepository memberRepository)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public async Task<PagedResponseDto<LoanResponseDto>> GetPagedAsync(ListQueryDto query)
    {
        var (loans, totalCount) =
            await _loanRepository.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.SortBy,
                query.SortDirection);

        var items = loans.Select(loan => new LoanResponseDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            BorrowedAt = loan.BorrowedAt,
            DueDate = loan.DueDate,
            ReturnedAt = loan.ReturnedAt
        }).ToList();

        return new PagedResponseDto<LoanResponseDto>
        {
            Items = items,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)query.PageSize)
        };
    }

    public async Task<LoanResponseDto?> GetByIdAsync(int id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
        {
            return null;
        }

        return MapToResponseDto(loan);
    }

    public async Task<LoanResponseDto> CreateAsync(LoanCreateDto dto)
    {
        var borrowedAt = DateTime.UtcNow;

        ValidateDates(borrowedAt, dto.DueDate, returnedAt: null);

        await ValidateBookAndMemberAsync(dto.BookId, dto.MemberId);

        var loan = new Loan
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowedAt = borrowedAt,
            DueDate = dto.DueDate,
            ReturnedAt = null
        };

        var createdLoan =
            await _loanRepository.CreateAsync(loan);

        return MapToResponseDto(createdLoan);
    }

    public async Task<bool> UpdateAsync(int id, LoanUpdateDto dto)
    {
        var loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
        {
            return false;
        }

        ValidateDates(dto.BorrowedAt, dto.DueDate, dto.ReturnedAt);

        await ValidateBookAndMemberAsync(dto.BookId, dto.MemberId);

        loan.BookId = dto.BookId;
        loan.MemberId = dto.MemberId;
        loan.BorrowedAt = dto.BorrowedAt;
        loan.DueDate = dto.DueDate;
        loan.ReturnedAt = dto.ReturnedAt;

        await _loanRepository.UpdateAsync(loan);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);

        if (loan is null)
        {
            return false;
        }

        await _loanRepository.DeleteAsync(loan);

        return true;
    }

    private async Task ValidateBookAndMemberAsync(int bookId, int memberId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book is null)
        {
            throw new KeyNotFoundException("Book was not found.");
        }

        var member =
            await _memberRepository.GetByIdAsync(memberId);

        if (member is null)
        {
            throw new KeyNotFoundException("Member was not found.");
        }

        if (!member.IsActive)
        {
            throw new InvalidOperationException("Inactive member cannot borrow a book.");
        }
    }

    private static void ValidateDates(DateTime borrowedAt, DateTime dueDate, DateTime? returnedAt)
    {
        if (dueDate <= borrowedAt)
        {
            throw new ArgumentException(
                "Due date must be later than borrowed date.");
        }

        if (returnedAt.HasValue &&
            returnedAt.Value < borrowedAt)
        {
            throw new ArgumentException(
                "Returned date cannot be earlier than borrowed date.");
        }
    }

    private static LoanResponseDto MapToResponseDto(Loan loan)
    {
        return new LoanResponseDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            BorrowedAt = loan.BorrowedAt,
            DueDate = loan.DueDate,
            ReturnedAt = loan.ReturnedAt
        };
    }
}