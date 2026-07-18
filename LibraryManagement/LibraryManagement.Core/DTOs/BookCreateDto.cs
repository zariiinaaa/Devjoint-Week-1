using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagement.Core.DTOs
{
    public class BookCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BookCode { get; set; } = string.Empty;

        [Range(1000, 9999)]
        public int PublishedYear { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }
    }
}
