using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualDeansOffice.Commands;
using VirtualDeansOffice.Models;
using VirtualDeansOffice.Queries;

namespace VirtualDeansOffice.Controllers
{
    public class GradesController : Controller
    {
        private readonly GetGradesHandler _getGrades;
        private readonly GetStudentsHandler _getStudents;
        private readonly GetCoursesHandler _getCourses;
        private readonly AddGradeHandler _addGrade;
        private readonly DeleteGradeHandler _deleteGrade;

        public GradesController(
            GetGradesHandler getGrades,
            GetStudentsHandler getStudents,
            GetCoursesHandler getCourses,
            AddGradeHandler addGrade,
            DeleteGradeHandler deleteGrade)
        {
            _getGrades = getGrades;
            _getStudents = getStudents;
            _getCourses = getCourses;
            _addGrade = addGrade;
            _deleteGrade = deleteGrade;
        }

        public IActionResult Index()
        {
            var grades = _getGrades.Handle(new GetGradesQuery());
            return View(grades);
        }

        public IActionResult Add()
        {
            FillSelectLists();
            // a new model so the form starts with today's date
            return View(new Grade());
        }

        [HttpPost]
        public IActionResult Add(Grade grade)
        {
            if (!ModelState.IsValid)
            {
                FillSelectLists();
                return View(grade);
            }

            _addGrade.Handle(new AddGradeCommand
            {
                StudentId = grade.StudentId,
                CourseId = grade.CourseId,
                Value = grade.Value,
                Date = grade.Date
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _deleteGrade.Handle(new DeleteGradeCommand { Id = id });
            return RedirectToAction("Index");
        }

        private void FillSelectLists()
        {
            var students = _getStudents.Handle(new GetStudentsQuery());
            var courses = _getCourses.Handle(new GetCoursesQuery());
            var studentItems = students.Select(s => new { s.Id, FullName = s.LastName + " " + s.FirstName + " (" + s.StudentNumber + ")" });
            ViewBag.Students = new SelectList(studentItems, "Id", "FullName");
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
        }
    }
}
