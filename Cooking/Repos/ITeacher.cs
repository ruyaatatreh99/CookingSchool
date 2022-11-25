using Cooking.Model;
namespace Cooking.Repos
{
    public interface ITeacher
    {
        Teacher signin(string email, string password);
        Class CreateClass(string teachername, int courseID, string coursename, string ClassTime);
        List<Request> ViewAllRequest(int TeacherID);
        void AcceptStudent(int studentId, int classId, int teacherID);
        Mark AddMark(double markvalue, string studentname, int classid);
        Mark updateMark(double markvalue, string studentname, int classid);
        List<Course> viewAllCourse();
        List<Class> viewAllTeacherClass(int teacherID);
        Class GetClass( int ClassID);
      //  List<string> viewMeal();
        void FavoriteMeal(int mealid);
       
    }
}
