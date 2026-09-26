using System.ComponentModel.DataAnnotations;

namespace VirtualDeansOffice.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nr albumu")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kierunek")]
        public string FieldOfStudy { get; set; }

        [Range(1, 5, ErrorMessage = "Pole {0} musi być w zakresie od {1} do {2}.")]
        [Display(Name = "Rok studiów")]
        public int YearOfStudy { get; set; } = 1;

        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
