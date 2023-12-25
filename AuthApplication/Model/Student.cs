using System.ComponentModel.DataAnnotations;

namespace AuthApplication.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; } = default!;
        [Required]
        public int Age { get; set; }
    }

    public class InputStudent
    {
        [Required]
        public string StudentName { get; set; } = default!;
        [Required]
        public int Age { get; set; }
    }

}
