using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;


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

        [Route("/signin")]
        [HttpPost]
        public IActionResult signin([FromBody] Dictionary<string, string> data)
        {
           

                Teacher Teacher = _teacher.signin(data["email"], data["password"]);
                if (Teacher == null) return NotFound(new { errors = "email or password invaild" });
                else return Ok(new { Teacher = Teacher });
           
        }

        [Route("/teacher/class")]
        [HttpPost]
        public IActionResult CreateClass([FromBody] Dictionary<string, string> data, int course_ID )
        {

                Class Class = _teacher.CreateClass(data["teacher_name"],course_ID, data["course_name"], data["Class_Time"]);
                if (Class == null) return NotFound(new { errors = "Class already Exists" });
                else return Ok(new { Class = Class });
           
        }

        [Route("/request")]
        [HttpGet]
        public IActionResult ViewAllRequest(int Teacher_ID)
        {
           

                var Request = _teacher.ViewAllRequest(Teacher_ID);
                return Ok(new { Request = Request });
           
        }

        [Route("/request")]
        [HttpPost]
        public IActionResult AcceptStudent([FromBody] Dictionary<string, string> data, int class_Id)
        {
            

                _teacher.AcceptStudent(Int16.Parse(data["student_Id"]), class_Id, Int16.Parse(data["teacher_ID"]));
                return Ok(new {});
           
        }

        [Route("/mark")]
        [HttpPost]
        public IActionResult AddMark([FromBody] Dictionary<string, string> data, int class_Id, string status)
        {
            
               Mark mark= _teacher.AddMark(Double.Parse(data["mark_value"]),data["student_name"],class_Id, status);
                if (mark == null) return NotFound(new { errors = "Already Added" });
                else return Ok(new { mark = mark });
           
        }

        [Route("/mark")]
        [HttpPut]
        public IActionResult updateMark([FromBody] Dictionary<string, string> data, int class_Id, string status)
        {
            
                Mark mark = _teacher.AddMark(Double.Parse(data["mark_value"]), data["student_name"], class_Id,status);
                return Ok(new { mark = mark });
           
        }

        [Route("/course")]
        [HttpGet]
        public IActionResult viewAllCourse()
        {
           
                var course = _teacher.viewAllCourse();
                return Ok(new { Courses = course });
            
        }

        [Route("/teacher/class")]
        [HttpGet]
        public IActionResult viewAllTeacherClass(int teacher_ID)
        {
            
                var Class = _teacher.viewAllTeacherClass( teacher_ID);
                return Ok(new { Classes = Class });

        }

        [Route("/teacher/class_teacher")]
        [HttpGet]
        public IActionResult GetClass(int Class_ID)
        {
           
                var Class = _teacher.GetClass(Class_ID);
                return Ok(new { Class = Class });

        }
        [Route("/teacher/class/student")]
        [HttpDelete]
        public IActionResult DeleteStudent(int Class_ID,int  student_ID )
        {

             _teacher.DeleteStudent(Class_ID, student_ID);
            return Ok(new { });

        }
    }
    }
