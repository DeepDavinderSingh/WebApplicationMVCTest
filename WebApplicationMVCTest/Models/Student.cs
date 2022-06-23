using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVCTest.Models
{
    public class Student
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z]([a-z A-Z]){2,60}", ErrorMessage = "Only Alphabets are allowed & Name must be of minimum 3-characters.")]
        public string? Name { get; set; }

        [Required]
        [Range(15, 50, ErrorMessage = "Age must be between 15-50 in years.")]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        [EmailAddress]
      
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string? Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage ="Phone No. must be of 10-Digits")]
        [MaxLength(10, ErrorMessage = "Phone No. must be of 10-Digits")]
        public long PhoneNo { get; set; }

        [Required]
        public string? Address { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }

    }
}
