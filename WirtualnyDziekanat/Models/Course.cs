using System.ComponentModel.DataAnnotations;

namespace VirtualDeansOffice.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Prowadzący")]
        public string Lecturer { get; set; }

        [Range(1, 30, ErrorMessage = "Pole {0} musi być w zakresie od {1} do {2}.")]
        public int ECTS { get; set; } = 5;

        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
