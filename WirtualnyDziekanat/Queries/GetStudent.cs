using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Queries
{
    public class GetStudentQuery
    {
        public int Id { get; set; }
    }

    public class GetStudentHandler : IQueryHandler<GetStudentQuery, Student>
    {
        private readonly DeansOfficeContext _context;

        public GetStudentHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        // returns the student with their grades, or null
        public Student Handle(GetStudentQuery query)
        {
            var student = _context.Students.AsNoTracking().FirstOrDefault(s => s.Id == query.Id);
            if (student == null)
            {
                return null;
            }

            student.Grades = _context.Grades.AsNoTracking()
                .Include(g => g.Course)
                .Where(g => g.StudentId == query.Id)
                .ToList();

            return student;
        }
    }
}
