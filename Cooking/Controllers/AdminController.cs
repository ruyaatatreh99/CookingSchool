using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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

                Admin admin = _inner.signin(data["email"], data["password"]);
            if (admin == null) return NotFound(new { errors = "email or password invaild" });
            else {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim("type","data"),
                     new Claim(ClaimTypes.Role,"Admin"),
                };
                    var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

                var Token = new JwtSecurityToken(
        "https://fbi-demo.com",
        "https://fbi-demo.com",
       claims,
        expires: DateTime.Now.AddDays(90),
        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    );

                var jwt = new JwtSecurityTokenHandler().WriteToken(Token);
                    return Ok(new {Admin=admin,Token=jwt });
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
                else
                {
                    return Ok(new { Admin = admin });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/admin/teacher")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
