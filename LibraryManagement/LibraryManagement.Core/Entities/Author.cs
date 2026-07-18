using LibraryManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagement.Core.Entities
{
    public class Author:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Biography { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
