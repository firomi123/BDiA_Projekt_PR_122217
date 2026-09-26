using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Queries
{
    public class GetGradesQuery
    {
    }

    public class GetGradesHandler : IQueryHandler<GetGradesQuery, List<Grade>>
    {
        private readonly DeansOfficeContext _context;

        public GetGradesHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public List<Grade> Handle(GetGradesQuery query)
        {
            return _context.Grades.AsNoTracking()
                .Include(g => g.Student)
                .Include(g => g.Course)
                .OrderByDescending(g => g.Date)
                .ToList();
        }
    }
}
