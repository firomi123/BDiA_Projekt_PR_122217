using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Commands
{
    public class AddGradeCommand
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class AddGradeHandler : ICommandHandler<AddGradeCommand>
    {
        private readonly DeansOfficeContext _context;

        public AddGradeHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(AddGradeCommand command)
        {
            var grade = new Grade
            {
                StudentId = command.StudentId,
                CourseId = command.CourseId,
                Value = command.Value,
                Date = command.Date
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();
        }
    }
}
