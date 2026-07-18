using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Core.Common;

namespace LibraryManagement.Core.Entities;

public class Loan : BaseEntity
{
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; } = null!;

    public int MemberId { get; set; }

    [ForeignKey(nameof(MemberId))]
    public Member Member { get; set; } = null!;

    public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;

    public DateTime DueDate { get; set; }

    public DateTime? ReturnedAt { get; set; }
}