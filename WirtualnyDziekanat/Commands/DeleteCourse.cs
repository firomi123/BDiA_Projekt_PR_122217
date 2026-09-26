using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Commands
{
    public class DeleteCourseCommand
    {
        public int Id { get; set; }
    }

    public class DeleteCourseHandler : ICommandHandler<DeleteCourseCommand>
    {
        private readonly DeansOfficeContext _context;

        public DeleteCourseHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        // deletes the course together with its grades
        public void Handle(DeleteCourseCommand command)
        {
            var course = _context.Courses.Find(command.Id);
            if (course == null)
            {
                return;
            }

            var grades = _context.Grades.Where(g => g.CourseId == command.Id).ToList();
            _context.Grades.RemoveRange(grades);
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}
