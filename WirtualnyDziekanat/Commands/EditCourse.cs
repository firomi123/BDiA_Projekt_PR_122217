using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Commands
{
    public class EditCourseCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int ECTS { get; set; }
    }

    public class EditCourseHandler : ICommandHandler<EditCourseCommand>
    {
        private readonly DeansOfficeContext _context;

        public EditCourseHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(EditCourseCommand command)
        {
            var course = _context.Courses.Find(command.Id);
            if (course == null)
            {
                return;
            }

            course.Name = command.Name;
            course.Lecturer = command.Lecturer;
            course.ECTS = command.ECTS;
            _context.SaveChanges();
        }
    }
}
