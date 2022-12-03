using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Timers;
using Cooking.Controllers;
using Cooking.Model;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace Cooking.Repos
{
    public class TeacherRepos : ITeacher
    {
        private DBContext _context;
        public TeacherRepos(DBContext context)
        {
            _context = context;
        }
        public void AcceptStudent(int studentId, int classId, int teacherID)
        {
            Request? getrequest = _context.Request.FirstOrDefault(x => x.StudentID == studentId && x.ClassId == classId && x.TeacherID == teacherID);
            Student? Student = _context.Student.FirstOrDefault(x => x.StudentID == studentId);
            Class? Class = _context.Class.FirstOrDefault(x => x.ClassId == classId);
            if (getrequest != null)
            {
                _context.Request.Remove(getrequest);
                _context.SaveChanges();
            }
            if (Class != null && Student != null)
            {
                Class.StudentNo++;
                _context.Class.Update(Class);
                _context.SaveChanges();
                StudentClass StudentClass = new StudentClass();
                StudentClass.StudentID = Student.StudentID;
                StudentClass.ClassId = Class.ClassId;
                _context.StudentClass.Add(StudentClass);
                _context.SaveChanges();

            }

        }

        public Mark AddMark(double markvalue, string studentname, int classid,string status)
        {
            Mark mark = new Mark();
            Student? getstudent = _context.Student.First(x => x.username == studentname);
            Mark? check = _context.Mark.FirstOrDefault(x => x.StudentID == getstudent.StudentID || x.ClassID == classid);
            if (check != null) return null;
            else
            {
                mark.ClassID = classid;
                mark.Markvalue = markvalue;
                mark.status = status;
                mark.StudentID = getstudent.StudentID;
                _context.Mark.Add(mark);
                _context.SaveChanges();
                getstudent.MarkList.Add(mark);
                _context.Student.Update(getstudent);
                _context.SaveChanges();
                return mark;
            }
        }

        public Class CreateClass(string teachername, int courseID, string coursename,string ClassTime)
        {
            Class c = new Class();
            Class? check = _context.Class.FirstOrDefault(x => x.CourseID == courseID || x.TeacherName == teachername);
            if (check != null) return null;
            else
            {
                c.TeacherName = teachername;
                c.StudentNo = 0;
                c.CourseID = courseID;
                c.CourseName = coursename;
                c.StudentClass = new List<StudentClass>();
                _context.Class.Add(c);
                _context.SaveChanges();
                return c;
            }
        }

        public void DeleteStudent(int ClassID, int studentID)
        {
            var student = _context.StudentClass.First(x => x.StudentID == studentID  &&  x.ClassId== ClassID);
            _context.StudentClass.Remove(student);
            _context.SaveChanges();
        }

        public void FavoriteMeal(int mealid)
        {
            throw new NotImplementedException();
        }

        public Class GetClass(int ClassID)
        {
            Class Class = _context.Class.First(x => x.ClassId == ClassID);
            return Class;
        }

        public Employee signin(string email, string password)
        {
            var Teacher = _context.Employee.FirstOrDefault(x => x.email == email);

            if (Teacher == null) return null;
            else
            {
                var decryptedpassword = Convert.FromBase64String(Teacher.password);
                var result = Encoding.UTF8.GetString(decryptedpassword);
                result = result.Substring(0, result.Length);
                if (result == password) return Teacher;
                else return null;

            }
        }

        public Mark updateMark(double markvalue, string studentname, int classid, string status)
        {
            Student? getstudent = _context.Student.First(x => x.username == studentname);
            Mark? mark = _context.Mark.First(x => x.StudentID == getstudent.StudentID && x.ClassID == classid);
            getstudent.MarkList.Remove(mark);
            mark.Markvalue = markvalue;
            mark.status = status;
            _context.Mark.Update(mark);
            _context.SaveChanges();
            getstudent.MarkList.Add(mark);
            _context.Student.Update(getstudent);
            _context.SaveChanges();
            return mark;
        }

        public List<Course> viewAllCourse()
        {
            List<Course> AllCourseList;
            AllCourseList = _context.Set<Course>().ToList();
            return AllCourseList;
        }

        public List<Request> ViewAllRequest(int TeacherID)
        {
            List<Request> AllRequestList;
            List<Request> TeacherRequestList = new List<Request>();
            AllRequestList = _context.Set<Request>().ToList();

            foreach (Request req in AllRequestList)
            {
                if (req.TeacherID == TeacherID) TeacherRequestList.Add(req);
            }
            return TeacherRequestList;
        }

        public List<Class> viewAllTeacherClass(int teacherID)
        {
            List<Class> AllClassList;
            List<Class> TeacherClasstList = new List<Class>();
            AllClassList = _context.Set<Class>().ToList();
            Employee? getTeacher = _context.Employee.First(x => x.EmpID == teacherID);
            foreach (Class Class in AllClassList)
            {
                if (Class.TeacherName == getTeacher.username) TeacherClasstList.Add(Class);
            }
            return TeacherClasstList;
        }

        /*     public List<string> viewMeal()
             {
                 string url = @"https://";

                 WebRequest request = WebRequest.Create(url);

                 WebResponse response = request.GetResponse();

                 Stream data = response.GetResponseStream();

                 StreamReader reader = new StreamReader(data);

                 // json-formatted string from maps api
                 string responseFromServer = reader.ReadToEnd();

                 response.Close();

             }*/
    }
}
