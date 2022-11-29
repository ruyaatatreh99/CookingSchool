using Cooking.Model;
namespace Cooking.Repos
{
    public interface IStudent
    {
        Student signup(string email, string password,string phone, string image, string username);
        Student signin(string email, string password);
        Exam SubmitTask(Exam Exam);
        void pickLevel(string level, int StudentId);
        void regiserClasses(int classID, int StudentId);
        List<Class> ViewAllClasses();
        List<StudentClass> viewRegiseredClasses(int StudentId);
        Favourite AddFavourite(int user_id, int meal_id);
        List<Mark> ShowClassMark(int StudentId);
        void deleteFavourite(int user_id, int meal_id);
        List<Favourite> getFavourite();
    }
}
