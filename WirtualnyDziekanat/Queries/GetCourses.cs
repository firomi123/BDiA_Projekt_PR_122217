using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Queries
{
    public class GetCoursesQuery
    {
    }

    public class GetCoursesHandler : IQueryHandler<GetCoursesQuery, List<Course>>
    {
        private readonly DeansOfficeContext _context;

        public GetCoursesHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public List<Course> Handle(GetCoursesQuery query)
        {
            return _context.Courses.AsNoTracking().OrderBy(c => c.Name).ToList();
        }
    }
}
