using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Commands
{
    public class EditStudentCommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }
        public string FieldOfStudy { get; set; }
        public int YearOfStudy { get; set; }
    }

    public class EditStudentHandler : ICommandHandler<EditStudentCommand>
    {
        private readonly DeansOfficeContext _context;

        public EditStudentHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(EditStudentCommand command)
        {
            var student = _context.Students.Find(command.Id);
            if (student == null)
            {
                return;
            }

            student.FirstName = command.FirstName;
            student.LastName = command.LastName;
            student.StudentNumber = command.StudentNumber;
            student.FieldOfStudy = command.FieldOfStudy;
            student.YearOfStudy = command.YearOfStudy;
            _context.SaveChanges();
        }
    }
}
