
using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;
namespace Cooking.Controllers
{
    public class StudentController : ControllerBase
    {

        private readonly IStudent _inner;
        public StudentController(IStudent inner)
        {
            _inner = inner;

        }

        [Route("user/signin")]
        [HttpPost]
        public IActionResult signin([FromBody] Dictionary<string, string> data)
        {


            Student Student = _inner.signin(data["email"], data["password"]);
            if (Student == null) return NotFound(new { errors = "email or password invaild" });
            else return Ok(new { Student = Student });

        }

        [Route("user/signup")]
        [HttpPost]
        public IActionResult signup([FromBody] Dictionary<string, string> data)
        {


            Student Student = _inner.signup(data["email"], data["password"], data["phone"], data["image"], data["username"]);
            if (Student == null) return NotFound(new { errors = " Student already Exist" });
            else return Ok(new { Student = Student });

        }

        [Route("user/level")]
        [HttpGet]
        public IActionResult pickLevel(string level, int Student_Id)
        {


            _inner.pickLevel(level,Student_Id);
             return Ok(new { });

        }

        [Route("user/register")]
        [HttpPost]
        public IActionResult regiserClasses([FromBody] Dictionary<string, string> data)
        {


           _inner.regiserClasses( Int16.Parse(data["class_ID"]), Int16.Parse(data["Student_Id"]));
            return Ok(new { message = "sent request.wait theacher to accept your request"});

        }

        [Route("user/classes")]
        [HttpGet]
        public IActionResult ViewAllClasses()
        {


            var Classes = _inner.ViewAllClasses();
            return Ok(new { Classes = Classes });

        }

        [Route("user/Student_classes")]
        [HttpGet]
        public IActionResult viewRegiseredClasses(int Student_Id)
        {


            var Classes = _inner.viewRegiseredClasses(Student_Id);
            var StudentClasstList = Classes.Where(a => a.StudentID == Student_Id);
            return Ok(new { Classes = StudentClasstList });

        }

        [Route("user/Student_Mark")]
        [HttpGet]
        public IActionResult ShowClassMark(int Student_Id)
        {


            var Mark = _inner.ShowClassMark( Student_Id);
            return Ok(new { Mark = Mark });

        }
    }
}
