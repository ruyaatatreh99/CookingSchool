using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cooking.Model
{
    public class DBContext : DbContext

    {
        public DBContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        
        public DbSet<Mark> Mark { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }

        protected readonly IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            if (!optionsbuilder.IsConfigured)
            {
                optionsbuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=school;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


            }
            base.OnConfiguring(optionsbuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentClass>().HasKey(sc => new { sc.StudentID, sc.ClassId });
        }
    }
    }
