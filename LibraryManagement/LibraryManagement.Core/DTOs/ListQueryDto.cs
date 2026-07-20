using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.DTOs;

public class ListQueryDto
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;

    public string SortBy { get; set; } = "id";

    public string SortDirection { get; set; } = "asc";
}