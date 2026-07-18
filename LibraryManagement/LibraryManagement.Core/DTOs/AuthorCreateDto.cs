using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.DTOs;

public class AuthorCreateDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Biography { get; set; }
}