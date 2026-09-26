using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Commands
{
    public class DeleteGradeCommand
    {
        public int Id { get; set; }
    }

    public class DeleteGradeHandler : ICommandHandler<DeleteGradeCommand>
    {
        private readonly DeansOfficeContext _context;

        public DeleteGradeHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public void Handle(DeleteGradeCommand command)
        {
            var grade = _context.Grades.Find(command.Id);
            if (grade == null)
            {
                return;
            }

            _context.Grades.Remove(grade);
            _context.SaveChanges();
        }
    }
}
