using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cooking.Model
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "password required")]
        [MinLength(9)]
       
        public string? password { get; set; }
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public string? username { get; set; }
        [Required]
        public string? phone { get; set; }
        [Required]
        public string? role { get; set; }
        public string? image { get; set; }
        public string? level { get; set; }
        public List<Course>  ?CourseList { get; set; }
        public List<Mark> ?MarkList { get; set; }
    }
}
