using Cooking.Model;
namespace Cooking.Repos
{
    public interface IStudent
    {
        Student signup(Admin admin);
        Student signin(string email, string password);
        void pickLevel(string level);
        Class regiserClasses(int classID, int StudentId);
        List<Class> ViewAllClasses();
        List<Class> viewRegiseredClasses(int StudentId);
        List<string> viewMeal();
        void FavoriteMeal(int mealid);
        List<Mark> ShowCourseMark();
    }
}
