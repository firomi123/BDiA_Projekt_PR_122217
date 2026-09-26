using Microsoft.AspNetCore.Mvc;
using VirtualDeansOffice.Commands;
using VirtualDeansOffice.Models;
using VirtualDeansOffice.Queries;

namespace VirtualDeansOffice.Controllers
{
    public class StudentsController : Controller
    {
        private readonly GetStudentsHandler _getStudents;
        private readonly GetStudentHandler _getStudent;
        private readonly AddStudentHandler _addStudent;
        private readonly EditStudentHandler _editStudent;
        private readonly DeleteStudentHandler _deleteStudent;

        public StudentsController(
            GetStudentsHandler getStudents,
            GetStudentHandler getStudent,
            AddStudentHandler addStudent,
            EditStudentHandler editStudent,
            DeleteStudentHandler deleteStudent)
        {
            _getStudents = getStudents;
            _getStudent = getStudent;
            _addStudent = addStudent;
            _editStudent = editStudent;
            _deleteStudent = deleteStudent;
        }

        public IActionResult Index(string search)
        {
            var students = _getStudents.Handle(new GetStudentsQuery { Search = search });
            return View(students);
        }

        public IActionResult Details(int id)
        {
            var student = _getStudent.Handle(new GetStudentQuery { Id = id });
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _addStudent.Handle(new AddStudentCommand
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentNumber = student.StudentNumber,
                FieldOfStudy = student.FieldOfStudy,
                YearOfStudy = student.YearOfStudy
            });
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _getStudent.Handle(new GetStudentQuery { Id = id });
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _editStudent.Handle(new EditStudentCommand
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentNumber = student.StudentNumber,
                FieldOfStudy = student.FieldOfStudy,
                YearOfStudy = student.YearOfStudy
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _deleteStudent.Handle(new DeleteStudentCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
