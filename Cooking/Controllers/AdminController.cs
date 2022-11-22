using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;

namespace Cooking.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _inner;


        public AdminController(IAdmin inner)
        {
            _inner = inner;

        }


        [Route("/admin/signin")]
        [HttpPost]
        public IActionResult signin([FromBody] Dictionary<string, string> data)
        {
            try
            {

                Admin admin = _inner.signin(data["email"], data["password"]);
                if (admin == null) return NotFound(new { errors = "email or password invaild" });
                else return Ok(new { admin = admin });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

       
        [Route("/admin/signup")]
        [HttpPost]
        public IActionResult signup([FromBody] Admin  data)
        {
            try
            {

                Admin admin = _inner.signup(data);
                if (admin == null) return NotFound(new { errors = "admin already Exist" });
                else return Ok(new { admin = admin });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

    
        [Route("/admin/teacher")]
        [HttpPost]
        public IActionResult CreateTeacherAccount([FromBody] Teacher teacher)
        {
            try
            {

                Teacher Teacher = _inner.CreateTeacherAccount( teacher);
                if (Teacher == null) return NotFound(new { errors = "Teacher already Exist" });
                else return Ok(new { Teacher = Teacher });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/course")]
        [HttpPost]
        public IActionResult CeateCourse(string course)
        {
            try
            {

                Course Course = _inner.CeateCourse( course);
                if (Course == null) return NotFound(new { errors = "Course already Exist" });
                else return Ok(new { Course = Course });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/teacher")]
        [HttpGet]
        public IActionResult GetTeacher(string username)
        {
            try
            {

                Teacher teacher = _inner.GetTeacher(username);
                if (teacher == null) return NotFound(new { errors = "teacher is nnot Exist" });
                else return Ok(new { teacher = teacher });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/student")]
        [HttpGet]
        public IActionResult GetStudent(string username)
        {
            try
            {

                Student student = _inner.GetStudent(username);
                if (student == null) return NotFound(new { errors = "student is not Exist" });
                else return Ok(new { student = student });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/all_teacher")]
        [HttpGet]
        public IActionResult GetAllTeacher()
        {
            try
            {

                var teacher = _inner.GetAllTeacher();
                return Ok(new { teachers = teacher, teachersCount = teacher.Count() });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/all_student")]
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            try
            {

                var Student = _inner.GetAllStudent();
                return Ok(new { Students = Student, StudentsCount = Student.Count() });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/course")]
        [HttpGet]
        public IActionResult GetAllCourse()
        {
            try
            {

                var course = _inner.GetAllCourse();
                return Ok(new { courses = course, coursesCount = course.Count() });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/course")]
        [HttpDelete]
        public IActionResult deleteCourse(int courseID)
        {
            try
            {
                _inner.deleteCourse(courseID);
                    return Ok(new { });
            }
            catch (Exception) { return new JsonResult(new { status = 500, message = "Error" }); }
        }

        [Route("/admin/teacher")]
        [HttpDelete]
        public IActionResult deleteTeacher(int teacherID)
        {
            try
            {
                _inner.deleteTeacher(teacherID);
                return Ok(new { });
            }
            catch (Exception) { return new JsonResult(new { status = 500, message = "Error" }); }
        }

    }
}
