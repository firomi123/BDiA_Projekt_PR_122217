using System.Globalization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VirtualDeansOffice.Data;
using VirtualDeansOffice.Models;
using VirtualDeansOffice.Commands;
using VirtualDeansOffice.Queries;

// so that grades like 4.5 work with a dot as the decimal separator
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// the database file path is resolved against the project folder, not the current working directory
var connection = new SqliteConnectionStringBuilder(builder.Configuration.GetConnectionString("DeansOffice"));
connection.DataSource = Path.Combine(builder.Environment.ContentRootPath, connection.DataSource);
builder.Services.AddDbContext<DeansOfficeContext>(options =>
    options.UseSqlite(connection.ToString()));

// CQRS - queries (read)
builder.Services.AddScoped<GetStudentsHandler>();
builder.Services.AddScoped<GetStudentHandler>();
builder.Services.AddScoped<GetCoursesHandler>();
builder.Services.AddScoped<GetCourseHandler>();
builder.Services.AddScoped<GetGradesHandler>();
builder.Services.AddScoped<StatisticsHandler>();

// CQRS - commands (write)
builder.Services.AddScoped<AddStudentHandler>();
builder.Services.AddScoped<EditStudentHandler>();
builder.Services.AddScoped<DeleteStudentHandler>();
builder.Services.AddScoped<AddCourseHandler>();
builder.Services.AddScoped<EditCourseHandler>();
builder.Services.AddScoped<DeleteCourseHandler>();
builder.Services.AddScoped<AddGradeHandler>();
builder.Services.AddScoped<DeleteGradeHandler>();

var app = builder.Build();

// create the database and seed sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DeansOfficeContext>();
    db.Database.EnsureCreated();

    if (!db.Students.Any())
    {
        db.Students.Add(new Student { FirstName = "Bartosz", LastName = "Niewiadomski", StudentNumber = "118452", FieldOfStudy = "Informatyka", YearOfStudy = 2 });
        db.Students.Add(new Student { FirstName = "Weronika", LastName = "Jakaś", StudentNumber = "121037", FieldOfStudy = "Informatyka", YearOfStudy = 2 });
        db.Students.Add(new Student { FirstName = "Kacper", LastName = "Nijaki", StudentNumber = "119864", FieldOfStudy = "Zarządzanie", YearOfStudy = 1 });

        db.Courses.Add(new Course { Name = "Programowanie obiektowe", Lecturer = "dr Grzegorz Pawlak", ECTS = 6 });
        db.Courses.Add(new Course { Name = "Bazy danych", Lecturer = "dr Monika Sobczak", ECTS = 5 });
        db.Courses.Add(new Course { Name = "Matematyka dyskretna", Lecturer = "prof. Tadeusz Kurek", ECTS = 4 });
        db.SaveChanges();

        db.Grades.Add(new Grade { StudentId = 1, CourseId = 1, Value = 4.5 });
        db.Grades.Add(new Grade { StudentId = 1, CourseId = 2, Value = 4.0 });
        db.Grades.Add(new Grade { StudentId = 2, CourseId = 1, Value = 5.0 });
        db.Grades.Add(new Grade { StudentId = 3, CourseId = 3, Value = 3.0 });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
