using Cooking.Model;

namespace Cooking.Repos
{
    public interface IAdmin
    {
        Employee signup(Employee admin);
        Employee signin(string email, string password);
        Employee GetTeacher(string username);
        List<Course> GetAllCourse();
        Student GetStudent(string username);
        List<Student> GetAllStudent();
        List<Employee> GetAllTeacher();
        Employee CreateTeacherAccount(Employee teacher);
        Course CeateCourse(string course);
        void deleteCourse(int courseID);
        void deleteTeacher(int teacherID);



    }
}
