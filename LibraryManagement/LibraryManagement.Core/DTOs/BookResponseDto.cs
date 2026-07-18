using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Core.DTOs
{
    public class BookResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string BookCode { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }
    }
}
