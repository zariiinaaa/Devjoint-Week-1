using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.DTOs;

public class LoanCreateDto
{
    [Range(1, int.MaxValue)]
    public int BookId { get; set; }

    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    public DateTime DueDate { get; set; }
}