using System;
using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;

        [Required]
        public Priority Priority { get; set; } = Priority.Low;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
