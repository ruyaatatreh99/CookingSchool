using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Timers;
using Cooking.Controllers;
using Cooking.Model;

namespace Cooking.Repos
{
    public class StudentRepos : IStudent
    {
        public void FavoriteMeal(int mealid)
        {
            throw new NotImplementedException();
        }

        public void pickLevel(string level)
        {
            throw new NotImplementedException();
        }

        public Class regiserClasses(int classID, int StudentId)
        {
            throw new NotImplementedException();
        }

        public List<Mark> ShowCourseMark()
        {
            throw new NotImplementedException();
        }

        public Student signin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Student signup(Admin admin)
        {
            throw new NotImplementedException();
        }

        public List<Class> ViewAllClasses()
        {
            throw new NotImplementedException();
        }

        public List<string> viewMeal()
        {
            throw new NotImplementedException();
        }

        public List<Class> viewRegiseredClasses(int StudentId)
        {
            throw new NotImplementedException();
        }
    }
}
