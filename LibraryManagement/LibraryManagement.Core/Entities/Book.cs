using System.ComponentModel.DataAnnotations;
using LibraryManagement.Core.Common;

namespace LibraryManagement.Core.Entities;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Range(1000, 9999)]
    public int PublishedYear { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalCopies { get; set; }

    [Range(0, int.MaxValue)]
    public int AvailableCopies { get; set; }

    public ICollection<Author> Authors { get; set; } = new List<Author>();

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}