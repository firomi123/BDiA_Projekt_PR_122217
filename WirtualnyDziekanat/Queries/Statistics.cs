using VirtualDeansOffice.CQRS;
using VirtualDeansOffice.Data;

namespace VirtualDeansOffice.Queries
{
    public class StatisticsQuery
    {
    }

    public class Statistics
    {
        public int StudentCount { get; set; }
        public int CourseCount { get; set; }
        public int GradeCount { get; set; }
    }

    public class StatisticsHandler : IQueryHandler<StatisticsQuery, Statistics>
    {
        private readonly DeansOfficeContext _context;

        public StatisticsHandler(DeansOfficeContext context)
        {
            _context = context;
        }

        public Statistics Handle(StatisticsQuery query)
        {
            return new Statistics
            {
                StudentCount = _context.Students.Count(),
                CourseCount = _context.Courses.Count(),
                GradeCount = _context.Grades.Count()
            };
        }
    }
}
