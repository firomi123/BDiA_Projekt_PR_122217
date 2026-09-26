using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Commands
{
    public class AddCourseCommand
    {
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int ECTS { get; set; }
    }

    public class AddCourseHandler : ICommandHandler<AddCourseCommand>
    {
        private readonly DeansOfficeContext _context;

        public AddCourseHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(AddCourseCommand command)
        {
            var course = new Course
            {
                Name = command.Name,
                Lecturer = command.Lecturer,
                ECTS = command.ECTS
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
        }
    }
}
