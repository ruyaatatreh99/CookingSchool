
using System.Text;
using Cooking.Model;

namespace Cooking.Repos
{
    public class StudentRepos : IStudent
    {
        private DBContext _context;
        public StudentRepos(DBContext context)
        {
            _context = context;
        }
        public Favourite AddFavourite(int user_id, int meal_id)
        {
            Favourite Favourite = new Favourite();
            Favourite check = _context.Favourite.FirstOrDefault(x => x.userid == user_id && x.mealid == meal_id);
            if (check != null) return null;
            else
            {
                
                Favourite.userid = user_id;
                Favourite.mealid = meal_id;
                _context.Favourite.Add(Favourite);
                _context.SaveChanges();
                return Favourite;
            }
        }

        public void deleteFavourite(int user_id, int meal_id)
        {
            var Favourite = _context.Favourite.First(x => x.userid == user_id  && x.mealid == meal_id);
            _context.Favourite.Remove(Favourite);
            _context.SaveChanges();
        }

        public List<Favourite> getFavourite()
        {
            List<Favourite> FavouriteList;
            try
            {
                FavouriteList = _context.Set<Favourite>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return FavouriteList;
        }

        public void pickLevel(string level, int StudentId)
        {
            var student = _context.Student.First(x => x.StudentID == StudentId);
            student.role = level;
            _context.Student.Update(student);
            _context.SaveChanges();
        }

        public void regiserClasses(int classID, int StudentId)
        {
            var student = _context.Student.First(x => x.StudentID == StudentId);
            var Class = _context.Class.First(x => x.ClassId == classID);
            var teacher = _context.Teacher.First(x => x.username == Class.TeacherName);
            Request Req = new Request();
            Req.StudentID = StudentId;
            Req.StudentNaame = student.username;
            Req.TeacherID = teacher.TeacherID;
            Req.ClassId = classID;
            _context.Request.Add(Req);
            _context.SaveChanges();
        }

        public List<Mark> ShowClassMark(int StudentId)
        {

            List<Mark> AllMarkList;
            List<Mark> StudentMarkList = new List<Mark>();
            AllMarkList = _context.Set<Mark>().ToList();
            Student? getStudent = _context.Student.First(x => x.StudentID == StudentId);
            foreach (Mark req in AllMarkList)
            {
                if (req.StudentID == getStudent.StudentID) StudentMarkList.Add(req);
            }
            return StudentMarkList;
        }

        public Student signin(string email, string password)
        {
            var Student = _context.Student.FirstOrDefault(x => x.email == email);

            if (Student == null) return null;
            else
            {
                var decryptedpassword = Convert.FromBase64String(Student.password);
                var result = Encoding.UTF8.GetString(decryptedpassword);
                result = result.Substring(0, result.Length);
                if (result == password) return Student;
                else return null;

            }
        }

        public Student signup(string email, string password0, string phone, string image,string username)
        {
            Student user = new Student();
            Student? checkemail = _context.Student.FirstOrDefault(x => x.email ==email || x.username == username);
            if (checkemail != null) return null;
            else
            {
                var password = Encoding.UTF8.GetBytes(password0);
                user.password = Convert.ToBase64String(password);
                user.email = email;
                user.username = username;
                if (image == "") user.image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAANlBMVEXk5ueutLersbTn6eqor7Lp6+y2u77h4+TIzM7N0dLR1NbU19mxt7q/xMbZ3N3c3+C6v8HCx8mJwGV6AAAFVUlEQVR4nO2d2XbjIAxAARmMd/v/f3YgSVs7kzY2CCMc3Zc50yffI7GDIgTDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMBcEQIt67NqmabtxEqAh9xdhAiBGO8vKoRy3f83S+b9fAYC6MapS8gknauxUviRAa/63+7E0jdC5vzEGEFZWv+k9JOVSF+sIYNWv4Vs5qkWUmau6kzv87o5NgWEEMG/yc03VF5equjvgd3MsLIywHBR0inPujz4ACLOzBa5RfV1KhwN1HyDoHacyFKEO0vNUYxGKdVgAC1I83MdsFOknKkRE0KPq3AZv0CZO0JFb4W/0EhlCF8SZcp5CG9UI71SWsGJMN7pSnHJ7/IqeUQylpDpFRclRj1qIKgKSoMvT3Cqv0QNSjjoMzSCihdDP3nLLvEDPeII0g1gjhtB1NvSCiNkKPTO9IOIKutVwbqEnoME2pDYmIqwpnsmt9MSEHEI3YHS5nTaARTdUA6klBuAnKbE0xVk2baG1iOoSGKqGUJpC/ObFCygN+kmaoZSEYog9oblTEdpYnFBn3d+GhEbEo4eF+1B0Nt0SjPc3w4FMV5OmK6W0DAbU5f0PPR3DNIOFVGQMdZ/GsKLT01zfMI0gG57IB2Tp9Q0vP1qkGvHpnCMC8n73F4RmbWlm3pLOzPv6q6dUK2BK50+X38VIM1woOkn6AbuJaXaECXU0n7Crf/2Tmeufrl3/hPQDTrkT3FSglaTi+rdNhB5wDemsnL7BvvVFLkmRb+7R2dBfgRlEUgunbz7gBi3eFVpKK8M10Fz9JjvexIaqINqLEpLdzB2UFxe0Vr7PIPSntF92+UOaaMXcCm+JfWFJ/xFp3NSmhIfAEKNYgqBTDN/RKEPw9iA/sOJAIYLBVSNkMVUjhD8zPdwY1VxWHR59dHZD+n3zS1ymHqrAQ38Y/B8Xxr1VlCpLdzXxF6CXPY6qGsqt2qbF8q7al1JDcSWiNmjR9L9Lqqq3ZVek84Ce7Muqgkr1y1hufm4AqLvFyEdhyFtpSGmWtr6I3gNnU09d29hbec/a/f9SeiuuKgagQW+5iKo386VnGzvMxvSPe6h9b+Zhse04uaZYrKn/8qmzg3mUnn0xEPoeR/Xz0tyaZe4PPoKXG5uhdwZ7pm3eVM62qwupLQxadNYFbk/l0u3gr/qhmahb6tvoflRuFU1n2QmykhrGQQbbrSxNUxOUdHOzQWGdrrnpagu0pqtaWDS9L0kzkgkkwDjvXeseoZKWxNYUiPZdQetg3No4e0lz/b5gd6Rj3mR1S/jAvd8DVKbL1ensLEiO4ThmcdTNOX43x/n8vRw9ps/PjeNybpcD4viufSRKndkcdXtegv5QnXawAWI+O4B3lGpPCaPucgTwTjWcEMaAivmIKJl64AguKI9G4t9Q0GNmP6+Y8jmUxrp8GIVKV11JZ22CK1SiVVXA2Xwq0lzaAKxS1hikUCQl6BQ7bEVigvjXp9CqreOB+0sY2EWCUcC80K8tmV50Dd5bb0hTMiGeHksR980WIlgl61LV10GgQilxSrKX+QLjHSbZRnjHIASRcAQlxvMTgkP9lujKCyPpHPVEPlXUub//PVXUHBztRWFKomqe6QIEo+p+p6rhhUzE/FQXIRgRRPRaF8kINUxVWhYd1QYqEtj+3UngmEh6yr0l9Icvc3/3fsJmp5CiQlkqgpYYBSVpaJrm/uojBA2JaepZJiLkET+0BSWpDHnFn6pybiICtocJ77C9IqR+XVEhDBkRi+poHMerLNLfoNlyfMzH+m3f0zhqCPbxVrAU6NTgZxiGYRiGYRiGYRiGYT6Uf/VaUTW8zb4SAAAAAElFTkSuQmCC";
                else user.image = image;
                user.phone = phone;
                user.role = "Student";
                user.MarkList= new List<Mark>();
                user.StudentClass = new List<StudentClass>();
                _context.Student.Add(user);
                _context.SaveChanges();
                return user;
            }
        }

        public Exam SubmitTask(Exam Exam)
        {
            _context.Exam.Add(Exam);
            _context.SaveChanges(true);
            return Exam;
        }

        public List<Class> ViewAllClasses()
        {
            List<Class> ClassList;
            try
            {
                ClassList = _context.Set<Class>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return ClassList;
        }

        public List<StudentClass> viewRegiseredClasses(int StudentId)
        {
            List<StudentClass> AllClassList;
            AllClassList = _context.Set<StudentClass>().ToList();
            return AllClassList;
        }
    }
}
