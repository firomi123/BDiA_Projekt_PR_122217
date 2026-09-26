using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.Models;

namespace VirtualDeansOffice.Data
{
    public class DeansOfficeContext : DbContext
    {
        public DeansOfficeContext(DbContextOptions<DeansOfficeContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
