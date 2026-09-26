using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Commands
{
    public class DeleteStudentCommand
    {
        public int Id { get; set; }
    }

    public class DeleteStudentHandler : ICommandHandler<DeleteStudentCommand>
    {
        private readonly DeansOfficeContext _context;

        public DeleteStudentHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        // deletes the student together with their grades
        public void Handle(DeleteStudentCommand command)
        {
            var student = _context.Students.Find(command.Id);
            if (student == null)
            {
                return;
            }

            var grades = _context.Grades.Where(g => g.StudentId == command.Id).ToList();
            _context.Grades.RemoveRange(grades);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
