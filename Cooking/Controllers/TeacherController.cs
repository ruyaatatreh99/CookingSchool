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
    public class TeacherController : ControllerBase
    {
          
            private readonly ITeacher _teacher;

            public TeacherController( ITeacher teacher1)
            {
                _teacher = teacher1;
            }

        [Route("/login")]
        [HttpPost]
        public IActionResult login([FromBody] Dictionary<string, string> data)
        {
            try
            {


                Employee Teacher = _teacher.signin(data["email"], data["password"]);
                if (Teacher == null) return NotFound(new { errors = "email or password invaild" });
                else
                {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim("type","data"),
                     new Claim(ClaimTypes.Role,"Teacher"),
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

                    var Token = new JwtSecurityToken(
            "https://fbi-demo.com",
            "https://fbi-demo.com",
           claims,
            expires: DateTime.Now.AddDays(90),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

                    var jwt = new JwtSecurityTokenHandler().WriteToken(Token);
                    return Ok(new { Teacher = Teacher,Toke= jwt });
                }
        }
            catch (Exception)
            {
                return new JsonResult(new { status = 500, message = "Error" });
            }
        }

        [Route("/teacher/class")]
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateClass([FromBody] Dictionary<string, string> data, int course_ID )
        {

                Class Class = _teacher.CreateClass(data["teacher_name"],course_ID, data["course_name"], data["Class_Time"]);
                if (Class == null) return NotFound(new { errors = "Class already Exists" });
                else return Ok(new { Class = Class });
           
        }

        [Route("/teacher/request")]
        [HttpGet]
        public IActionResult ViewAllRequest(int Teacher_ID)
        {
           

                var Request = _teacher.ViewAllRequest(Teacher_ID);
                return Ok(new { Request = Request });
           
        }

        [Route("/teacher/request")]
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult AcceptStudent([FromBody] Dictionary<string, string> data, int class_Id)
        {
            

                _teacher.AcceptStudent(Int16.Parse(data["student_Id"]), class_Id, Int16.Parse(data["teacher_ID"]));
                return Ok(new {});
           
        }

        [Route("/teacher/{class_id}/mark")]
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult AddMark([FromBody] Dictionary<string, string> data, int class_id, string status)
        {
            
               Mark mark= _teacher.AddMark(Double.Parse(data["mark_value"]),data["student_name"],class_id, status);
                if (mark == null) return NotFound(new { errors = "Already Added" });
                else return Ok(new { mark = mark });
           
        }

        [Route("/teacher/{class_id}/mark")]
        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public IActionResult updateMark([FromBody] Dictionary<string, string> data, int class_id, string status)
        {
            
                Mark mark = _teacher.AddMark(Double.Parse(data["mark_value"]), data["student_name"], class_id,status);
                return Ok(new { mark = mark });
           
        }

        [Route("/teacher/course")]
        [HttpGet]
        public IActionResult viewAllCourse()
        {
           
                var course = _teacher.viewAllCourse();
                return Ok(new { Courses = course });
            
        }

        [Route("/teacher/{teacher_id}/class")]
        [HttpGet]
        public IActionResult viewAllTeacherClass(int teacher_id)
        {
            
                var Class = _teacher.viewAllTeacherClass( teacher_id);
                return Ok(new { Classes = Class });

        }

        [Route("/teacher/class/{class_id}")]
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetClass(int class_id)
        {
           
                var Class = _teacher.GetClass(class_id);
                return Ok(new { Class = Class });

        }
       
        [Route("/teacher/class/student")]
        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteStudent(int Class_ID,int  student_ID )
        {

             _teacher.DeleteStudent(Class_ID, student_ID);
            return Ok(new { });

        }
    }
    }
