using System.ComponentModel.DataAnnotations;
using LibraryManagement.Core.Common;

namespace LibraryManagement.Core.Entities;

public class Member : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public DateTime MembershipDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}