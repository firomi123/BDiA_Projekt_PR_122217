using Microsoft.AspNetCore.Mvc;
using VirtualDeansOffice.Commands;
using VirtualDeansOffice.Models;
using VirtualDeansOffice.Queries;

namespace VirtualDeansOffice.Controllers
{
    public class CoursesController : Controller
    {
        private readonly GetCoursesHandler _getCourses;
        private readonly GetCourseHandler _getCourse;
        private readonly AddCourseHandler _addCourse;
        private readonly EditCourseHandler _editCourse;
        private readonly DeleteCourseHandler _deleteCourse;

        public CoursesController(
            GetCoursesHandler getCourses,
            GetCourseHandler getCourse,
            AddCourseHandler addCourse,
            EditCourseHandler editCourse,
            DeleteCourseHandler deleteCourse)
        {
            _getCourses = getCourses;
            _getCourse = getCourse;
            _addCourse = addCourse;
            _editCourse = editCourse;
            _deleteCourse = deleteCourse;
        }

        public IActionResult Index()
        {
            var courses = _getCourses.Handle(new GetCoursesQuery());
            return View(courses);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            _addCourse.Handle(new AddCourseCommand
            {
                Name = course.Name,
                Lecturer = course.Lecturer,
                ECTS = course.ECTS
            });
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var course = _getCourse.Handle(new GetCourseQuery { Id = id });
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            _editCourse.Handle(new EditCourseCommand
            {
                Id = course.Id,
                Name = course.Name,
                Lecturer = course.Lecturer,
                ECTS = course.ECTS
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _deleteCourse.Handle(new DeleteCourseCommand { Id = id });
            return RedirectToAction("Index");
        }
    }
}
