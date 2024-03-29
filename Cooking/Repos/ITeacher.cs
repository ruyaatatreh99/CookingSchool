﻿using Cooking.Model;
namespace Cooking.Repos
{
    public interface ITeacher
    {
        Employee signin(string email, string password);
        Class CreateClass(string teachername, int courseID, string coursename);
        List<Request> ViewAllRequest(int TeacherID);
        void AcceptStudent(int requests_id,int studentId, int classId, int teacherID);
        void rejectStudent(int requests_id);
        Mark AddMark(double markvalue, int studentid, int classid, string status);
        Mark updateMark(double markvalue, int studentid, int classid, string status);
        List<Course> viewAllCourse();
        List<Class> viewAllTeacherClass(int teacherID);
        Class GetClass( int ClassID);
        void FavoriteMeal(int mealid);
        void DeleteStudent(int ClassID, int studentID);


    }
}
