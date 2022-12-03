using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cooking.Model
{
    [Table("Class")]
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Teacher Name is required")]
        public string? TeacherName { get; set; }
        [Required]
        public int? StudentNo { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        public string? CourseName { get; set; }
        [Required]
        [ForeignKey("Course")]
        public int? CourseID { get; set; }
        public List<StudentClass> ? StudentClass { get; set; }
    }
}
