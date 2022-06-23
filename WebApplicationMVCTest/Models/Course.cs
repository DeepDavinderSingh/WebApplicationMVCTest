using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVCTest.Models
{
    public class Course
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string? courseName { get; set; }

        [Required]
        public string? courseDuration { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
