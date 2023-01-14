using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Data;
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

        [Route("admins/login")]
        [HttpPost]
        public IActionResult login([FromBody] Dictionary<string, string> data)
        {

            Employee admin = _inner.signin(data["email"], data["password"]);
            if (admin == null) return NotFound(new { errors = "email or password invaild" });
            else {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,data["email"]),
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
  
        [Route("admins/")]
        [HttpPost]
        public IActionResult Register([FromBody] Employee data)
        {
            try
            {

                Employee admin = _inner.signup(data);
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

        [Route("admins/teachers")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTeacherAccount([FromBody] Employee teacher)
        {
            try
            {

                Employee Teacher = _inner.CreateTeacherAccount(teacher);
                if (Teacher == null) return NotFound(new { errors = "Teacher already Exist" });
                else return Ok(new { Teacher = Teacher });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("admins/courses")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CeateCourse(string course)
        {
            try
            {

                Course Course = _inner.CeateCourse(course);
                if (Course == null) return NotFound(new { errors = "Course already Exist" });
                else return Ok(new { Course = Course });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("admins/teachers/{username}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTeacher(string username)
        {
            try
            {

                Employee teacher = _inner.GetTeacher(username);
                if (teacher == null) return NotFound(new { errors = "teacher is nnot Exist" });
                else return Ok(new { teacher = teacher });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("admins/students/{username}")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [Route("admins/teachers/all")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [Route("admins/courses/all")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllCourse()
        {
            try
            {

                var Course = _inner.GetAllCourse(); 
                return Ok(new { Courses = Course, CoursesCount = Course.Count() });
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }
        
        [Route("admins/students/all")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [Route("admins/courses/{course_id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult deleteCourse(int course_id)
        {
            try
            {
                _inner.deleteCourse(course_id);
                    return Ok(new { });
            }
            catch (Exception) { return new JsonResult(new { status = 500, message = "Error" }); }
        }

        [Route("admins/teachers/{teacher_id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult deleteTeacher(int teacher_id)
        {
            try
            {
                _inner.deleteTeacher(teacher_id);
                return Ok(new { });
            }
            catch (Exception) { return new JsonResult(new { status = 500, message = "Error" }); }
        }

    }
}
