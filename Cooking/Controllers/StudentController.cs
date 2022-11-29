
using Microsoft.AspNetCore.Mvc;
using Cooking.Model;
using Cooking.Repos;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Text.Json.Nodes;
using System.Net;

namespace Cooking.Controllers
{
    public class StudentController : ControllerBase
    {
        static HttpClient client = new HttpClient();

        private readonly IStudent _inner;
        public static IWebHostEnvironment _environment;
        public StudentController(IStudent inner, IWebHostEnvironment environment)
        {
            _inner = inner;
            _environment = environment;

        }

        [Route("user/submit_form")]
        [HttpPost]
        public Task <Exam> SubmitTask([FromForm] Exam Exam_File)
        {
            try
            {
               Exam obj = new Exam();
                obj.StudentID = Exam_File.StudentID;
                obj.ClassId = Exam_File.ClassId;
                obj.title =  Exam_File.title;
                obj.description = Exam_File.description;
            if (Exam_File.photo.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + Exam_File.photo.FileName))
                    {
                        Exam_File.photo.CopyTo(filestream);
                        filestream.Flush();
                        obj.photo = Exam_File.photo;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            var Exam = _inner.SubmitTask(Exam_File);
            return Task.FromResult(Exam);

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

        [Route("/Meal/search")]
        [HttpGet]
        public async Task<string> SearchMealByName(string Name)
        {
            string path = "http://www.themealdb.com/api/json/v1/1/search.php?s="+ Name;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        [Route("/Meal/List_meals")]
        [HttpGet]
        public async Task<string> List_meals(string Name)
        {
            string path = "http://www.themealdb.com/api/json/v1/1/search.php?f=" + Name;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        [Route("/Meal/Category")]
        [HttpGet]
        public async Task<string> List_Category()
        {
            string path = "http://www.themealdb.com/api/json/v1/1/categories.php";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        [Route("/Meal/Category/{name}")]
        [HttpGet]
        public async Task<string> List_Category(string name)
        {
            string path = "http://www.themealdb.com/api/json/v1/1/filter.php?c="+ name;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        [Route("/Meal/Area/{name}")]
        [HttpGet]
        public async Task<string> List_Area(string name)
        {
            string path = "http://www.themealdb.com/api/json/v1/1/filter.php?a=" + name;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }

        [Route("/Meal/Ingredients")]
        [HttpGet]
        public async Task<string> List_Ingredients()
        {
            string path = "http://www.themealdb.com/api/json/v1/1/list.php?i=list";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }
        [Route("/Meal/favourite")]
        [HttpPost]
        public IActionResult Add_Favourite(int user_id,int meal_id)
        {

           var f= _inner.AddFavourite(user_id, meal_id);
            if(f==null) return Ok(new { message = "Already Added " });
          else  return Ok(new {message="Added successfuly"});

        }
        [Route("/Meal/favourite")]
        [HttpDelete]
        public IActionResult delete_Favourite(int user_id, int meal_id)
        {

            _inner.deleteFavourite(user_id, meal_id);
             return Ok(new { message = "deleted successfuly" });

        }

        [Route("/Meal/favourite")]
        [HttpGet]
        public async Task<string> get_Favourite(int user_id)
        {
            var item="";
            string path = "";
            var f = _inner.getFavourite();
            List<Favourite> FavouriteList = new List<Favourite>();
            foreach (Favourite fav in f)
            {
                if (fav.userid == user_id) FavouriteList.Add(fav);
            }
            foreach (Favourite fav in FavouriteList)
            {
                path = "http://www.themealdb.com/api/json/v1/1/lookup.php?i="+fav.mealid;
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                response.EnsureSuccessStatusCode();
                item += await response.Content.ReadAsStringAsync();
            }
          
            return item;
        }
    }
}
