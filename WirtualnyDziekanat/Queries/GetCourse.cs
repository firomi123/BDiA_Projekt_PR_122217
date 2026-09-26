using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Queries
{
    public class GetCourseQuery
    {
        public int Id { get; set; }
    }

    public class GetCourseHandler : IQueryHandler<GetCourseQuery, Course>
    {
        private readonly DeansOfficeContext _context;

        public GetCourseHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public Course Handle(GetCourseQuery query)
        {
            return _context.Courses.AsNoTracking().FirstOrDefault(c => c.Id == query.Id);
        }
    }
}
