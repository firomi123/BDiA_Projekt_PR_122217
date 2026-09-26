using System.ComponentModel.DataAnnotations;

namespace VirtualDeansOffice.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Display(Name = "Przedmiot")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Range(2.0, 5.0, ErrorMessage = "Ocena musi być w zakresie od {1} do {2}.")]
        [Display(Name = "Ocena")]
        public double Value { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
