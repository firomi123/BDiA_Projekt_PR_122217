using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Queries
{
    public class GetStudentsQuery
    {
        public string Search { get; set; }
    }

    public class GetStudentsHandler : IQueryHandler<GetStudentsQuery, List<Student>>
    {
        private readonly DeansOfficeContext _context;

        public GetStudentsHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public List<Student> Handle(GetStudentsQuery query)
        {
            var students = _context.Students.AsNoTracking().ToList();

            if (!string.IsNullOrEmpty(query.Search))
            {
                students = students.Where(s => s.LastName.Contains(query.Search) || s.StudentNumber.Contains(query.Search)).ToList();
            }

            return students.OrderBy(s => s.LastName).ToList();
        }
    }
}
