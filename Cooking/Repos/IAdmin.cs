using Cooking.Model;

namespace Cooking.Repos
{
    public interface IAdmin
    {
        Admin signup(Admin  admin);
        Admin signin(string email, string password);
        Teacher GetTeacher(string username);
        List<Course> GetAllCourse();
        Student GetStudent(string username);
        List<Student> GetAllStudent();
        List<Teacher> GetAllTeacher();
        Teacher CreateTeacherAccount(Teacher teacher);
        Course CeateCourse(string course);
        void deleteCourse(int courseID);
        void deleteTeacher(int teacherID);



    }
}
