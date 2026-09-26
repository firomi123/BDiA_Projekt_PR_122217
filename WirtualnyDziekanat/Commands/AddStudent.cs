using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Commands
{
    public class AddStudentCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }
        public string FieldOfStudy { get; set; }
        public int YearOfStudy { get; set; }
    }

    public class AddStudentHandler : ICommandHandler<AddStudentCommand>
    {
        private readonly DeansOfficeContext _context;

        public AddStudentHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(AddStudentCommand command)
        {
            var student = new Student
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                StudentNumber = command.StudentNumber,
                FieldOfStudy = command.FieldOfStudy,
                YearOfStudy = command.YearOfStudy
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }
    }
}
