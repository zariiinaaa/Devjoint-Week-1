namespace LibraryManagement.Core.DTOs;

public class MemberResponseDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public DateTime MembershipDate { get; set; }

    public bool IsActive { get; set; }
}