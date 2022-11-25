using Cooking.Model;
namespace Cooking.Repos
{
    public interface IStudent
    {
        Student signup(string email, string password,string phone, string image, string username);
        Student signin(string email, string password);
        void pickLevel(string level, int StudentId);
        void regiserClasses(int classID, int StudentId);
        List<Class> ViewAllClasses();
        List<StudentClass> viewRegiseredClasses(int StudentId);
        List<string> viewMeal();
        void FavoriteMeal(int mealid);
        List<Mark> ShowClassMark(int StudentId);
    }
}
