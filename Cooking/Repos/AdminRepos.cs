
using System.Text;
using Cooking.Model;

namespace Cooking.Repos
{
    public class AdminRepos : IAdmin
    {
        private DBContext _context;
        public AdminRepos(DBContext context)
        {
            _context = context;
        }
        public Course CeateCourse(string course)
        {
            Course c = new Course();
            Course? check = _context.Course.FirstOrDefault(x => x.CourseName == course);
            if (check != null) return null;
            else
            {
                c.CourseName = course;        
                _context.Course.Add(c);
                _context.SaveChanges();
                return c;
            }
        }

        public Teacher CreateTeacherAccount(Teacher teacher)
        {
            Teacher u = new Teacher();
            Teacher? checkemail = _context.Teacher.FirstOrDefault(x => x.email == teacher.email || x.username == teacher.username);
            if (checkemail != null) return null;
            else
            {
                var password = Encoding.UTF8.GetBytes(teacher.password);
                u.password = Convert.ToBase64String(password);
                u.email = teacher.email;
                u.username = teacher.username;
                if (teacher.image == "") u.image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAANlBMVEXk5ueutLersbTn6eqor7Lp6+y2u77h4+TIzM7N0dLR1NbU19mxt7q/xMbZ3N3c3+C6v8HCx8mJwGV6AAAFVUlEQVR4nO2d2XbjIAxAARmMd/v/f3YgSVs7kzY2CCMc3Zc50yffI7GDIgTDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMBcEQIt67NqmabtxEqAh9xdhAiBGO8vKoRy3f83S+b9fAYC6MapS8gknauxUviRAa/63+7E0jdC5vzEGEFZWv+k9JOVSF+sIYNWv4Vs5qkWUmau6kzv87o5NgWEEMG/yc03VF5equjvgd3MsLIywHBR0inPujz4ACLOzBa5RfV1KhwN1HyDoHacyFKEO0vNUYxGKdVgAC1I83MdsFOknKkRE0KPq3AZv0CZO0JFb4W/0EhlCF8SZcp5CG9UI71SWsGJMN7pSnHJ7/IqeUQylpDpFRclRj1qIKgKSoMvT3Cqv0QNSjjoMzSCihdDP3nLLvEDPeII0g1gjhtB1NvSCiNkKPTO9IOIKutVwbqEnoME2pDYmIqwpnsmt9MSEHEI3YHS5nTaARTdUA6klBuAnKbE0xVk2baG1iOoSGKqGUJpC/ObFCygN+kmaoZSEYog9oblTEdpYnFBn3d+GhEbEo4eF+1B0Nt0SjPc3w4FMV5OmK6W0DAbU5f0PPR3DNIOFVGQMdZ/GsKLT01zfMI0gG57IB2Tp9Q0vP1qkGvHpnCMC8n73F4RmbWlm3pLOzPv6q6dUK2BK50+X38VIM1woOkn6AbuJaXaECXU0n7Crf/2Tmeufrl3/hPQDTrkT3FSglaTi+rdNhB5wDemsnL7BvvVFLkmRb+7R2dBfgRlEUgunbz7gBi3eFVpKK8M10Fz9JjvexIaqINqLEpLdzB2UFxe0Vr7PIPSntF92+UOaaMXcCm+JfWFJ/xFp3NSmhIfAEKNYgqBTDN/RKEPw9iA/sOJAIYLBVSNkMVUjhD8zPdwY1VxWHR59dHZD+n3zS1ymHqrAQ38Y/B8Xxr1VlCpLdzXxF6CXPY6qGsqt2qbF8q7al1JDcSWiNmjR9L9Lqqq3ZVek84Ce7Muqgkr1y1hufm4AqLvFyEdhyFtpSGmWtr6I3gNnU09d29hbec/a/f9SeiuuKgagQW+5iKo386VnGzvMxvSPe6h9b+Zhse04uaZYrKn/8qmzg3mUnn0xEPoeR/Xz0tyaZe4PPoKXG5uhdwZ7pm3eVM62qwupLQxadNYFbk/l0u3gr/qhmahb6tvoflRuFU1n2QmykhrGQQbbrSxNUxOUdHOzQWGdrrnpagu0pqtaWDS9L0kzkgkkwDjvXeseoZKWxNYUiPZdQetg3No4e0lz/b5gd6Rj3mR1S/jAvd8DVKbL1ensLEiO4ThmcdTNOX43x/n8vRw9ps/PjeNybpcD4viufSRKndkcdXtegv5QnXawAWI+O4B3lGpPCaPucgTwTjWcEMaAivmIKJl64AguKI9G4t9Q0GNmP6+Y8jmUxrp8GIVKV11JZ22CK1SiVVXA2Xwq0lzaAKxS1hikUCQl6BQ7bEVigvjXp9CqreOB+0sY2EWCUcC80K8tmV50Dd5bb0hTMiGeHksR980WIlgl61LV10GgQilxSrKX+QLjHSbZRnjHIASRcAQlxvMTgkP9lujKCyPpHPVEPlXUub//PVXUHBztRWFKomqe6QIEo+p+p6rhhUzE/FQXIRgRRPRaF8kINUxVWhYd1QYqEtj+3UngmEh6yr0l9Icvc3/3fsJmp5CiQlkqgpYYBSVpaJrm/uojBA2JaepZJiLkET+0BSWpDHnFn6pybiICtocJ77C9IqR+XVEhDBkRi+poHMerLNLfoNlyfMzH+m3f0zhqCPbxVrAU6NTgZxiGYRiGYRiGYRiGYT6Uf/VaUTW8zb4SAAAAAElFTkSuQmCC";
               else u.image = teacher.image;
                u.phone = teacher.phone;
                u.role = "Teacher";
                _context.Teacher.Add(u);
                _context.SaveChanges();
                return u;
            }
        }

        public void deleteCourse(int courseID)
        {
            var course = _context.Course.First(x => x.CourseID == courseID);
            _context.Course.Remove(course);
            _context.SaveChanges();
        }

        public void deleteTeacher(int teacherID)
        {
            var Teacher = _context.Teacher.First(x => x.TeacherID == teacherID);
            _context.Teacher.Remove(Teacher);
            _context.SaveChanges();
        }

        public List<Course> GetAllCourse()
        {
            List<Course> CourseList;
            try
            {
                CourseList = _context.Set<Course>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return CourseList;
        }

        public List<Student> GetAllStudent()
        {
            List<Student> StudentList;
            try
            {
                StudentList = _context.Set<Student>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return StudentList;
        }

        public List<Teacher> GetAllTeacher()
        {
            List<Teacher> TeacherList;
            try
            {
                TeacherList = _context.Set<Teacher>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return TeacherList;
        }

        public Student GetStudent(string username)
        {
            var student = _context.Student.FirstOrDefault(x => x.username == username);

            if (student == null) return null;
            else return student;
        }

        public Teacher GetTeacher(string username)
        {
            var Teacher = _context.Teacher.FirstOrDefault(x => x.username == username);

            if (Teacher == null) return null;
            else {
                var decryptedpassword = Convert.FromBase64String(Teacher.password);
                var result = Encoding.UTF8.GetString(decryptedpassword);
                result = result.Substring(0, result.Length);
                Teacher.password = result;
                return Teacher; }
        }

        public Admin signin(string email, string password)
        {
            var admin= _context.Admin.FirstOrDefault(x => x.email == email);

            if (admin == null) return null;
            else
            {
                var decryptedpassword = Convert.FromBase64String(admin.password);
                var result = Encoding.UTF8.GetString(decryptedpassword);
                result = result.Substring(0, result.Length);
                if (result == password) return admin;
                else return null;

            }
        }

        public Admin signup(Admin admin)
        {
            Admin u = new Admin();
            Admin ?checkemail = _context.Admin.FirstOrDefault(x => x.email == admin.email || x.username == admin.username);
            if (checkemail != null) return null;
            else
            {

                var password = Encoding.UTF8.GetBytes(admin.password);
                u.password = Convert.ToBase64String(password);
                u.email = admin.email;
                u.username = admin.username;
                if (admin.image == "") u.image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAANlBMVEXk5ueutLersbTn6eqor7Lp6+y2u77h4+TIzM7N0dLR1NbU19mxt7q/xMbZ3N3c3+C6v8HCx8mJwGV6AAAFVUlEQVR4nO2d2XbjIAxAARmMd/v/f3YgSVs7kzY2CCMc3Zc50yffI7GDIgTDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMAzDMBcEQIt67NqmabtxEqAh9xdhAiBGO8vKoRy3f83S+b9fAYC6MapS8gknauxUviRAa/63+7E0jdC5vzEGEFZWv+k9JOVSF+sIYNWv4Vs5qkWUmau6kzv87o5NgWEEMG/yc03VF5equjvgd3MsLIywHBR0inPujz4ACLOzBa5RfV1KhwN1HyDoHacyFKEO0vNUYxGKdVgAC1I83MdsFOknKkRE0KPq3AZv0CZO0JFb4W/0EhlCF8SZcp5CG9UI71SWsGJMN7pSnHJ7/IqeUQylpDpFRclRj1qIKgKSoMvT3Cqv0QNSjjoMzSCihdDP3nLLvEDPeII0g1gjhtB1NvSCiNkKPTO9IOIKutVwbqEnoME2pDYmIqwpnsmt9MSEHEI3YHS5nTaARTdUA6klBuAnKbE0xVk2baG1iOoSGKqGUJpC/ObFCygN+kmaoZSEYog9oblTEdpYnFBn3d+GhEbEo4eF+1B0Nt0SjPc3w4FMV5OmK6W0DAbU5f0PPR3DNIOFVGQMdZ/GsKLT01zfMI0gG57IB2Tp9Q0vP1qkGvHpnCMC8n73F4RmbWlm3pLOzPv6q6dUK2BK50+X38VIM1woOkn6AbuJaXaECXU0n7Crf/2Tmeufrl3/hPQDTrkT3FSglaTi+rdNhB5wDemsnL7BvvVFLkmRb+7R2dBfgRlEUgunbz7gBi3eFVpKK8M10Fz9JjvexIaqINqLEpLdzB2UFxe0Vr7PIPSntF92+UOaaMXcCm+JfWFJ/xFp3NSmhIfAEKNYgqBTDN/RKEPw9iA/sOJAIYLBVSNkMVUjhD8zPdwY1VxWHR59dHZD+n3zS1ymHqrAQ38Y/B8Xxr1VlCpLdzXxF6CXPY6qGsqt2qbF8q7al1JDcSWiNmjR9L9Lqqq3ZVek84Ce7Muqgkr1y1hufm4AqLvFyEdhyFtpSGmWtr6I3gNnU09d29hbec/a/f9SeiuuKgagQW+5iKo386VnGzvMxvSPe6h9b+Zhse04uaZYrKn/8qmzg3mUnn0xEPoeR/Xz0tyaZe4PPoKXG5uhdwZ7pm3eVM62qwupLQxadNYFbk/l0u3gr/qhmahb6tvoflRuFU1n2QmykhrGQQbbrSxNUxOUdHOzQWGdrrnpagu0pqtaWDS9L0kzkgkkwDjvXeseoZKWxNYUiPZdQetg3No4e0lz/b5gd6Rj3mR1S/jAvd8DVKbL1ensLEiO4ThmcdTNOX43x/n8vRw9ps/PjeNybpcD4viufSRKndkcdXtegv5QnXawAWI+O4B3lGpPCaPucgTwTjWcEMaAivmIKJl64AguKI9G4t9Q0GNmP6+Y8jmUxrp8GIVKV11JZ22CK1SiVVXA2Xwq0lzaAKxS1hikUCQl6BQ7bEVigvjXp9CqreOB+0sY2EWCUcC80K8tmV50Dd5bb0hTMiGeHksR980WIlgl61LV10GgQilxSrKX+QLjHSbZRnjHIASRcAQlxvMTgkP9lujKCyPpHPVEPlXUub//PVXUHBztRWFKomqe6QIEo+p+p6rhhUzE/FQXIRgRRPRaF8kINUxVWhYd1QYqEtj+3UngmEh6yr0l9Icvc3/3fsJmp5CiQlkqgpYYBSVpaJrm/uojBA2JaepZJiLkET+0BSWpDHnFn6pybiICtocJ77C9IqR+XVEhDBkRi+poHMerLNLfoNlyfMzH+m3f0zhqCPbxVrAU6NTgZxiGYRiGYRiGYRiGYT6Uf/VaUTW8zb4SAAAAAElFTkSuQmCC";
                else u.image = admin.image;
                u.phone = admin.phone;
                u.role = "Admin";
                _context.Admin.Add(u);
                _context.SaveChanges();
                return u;
            }
        }
    }
}
