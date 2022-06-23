using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVCTest.Models
{
    public class Enrollment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long CourseID { get; set; }
        [Required]
        public long StudentID { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
