using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cooking.Model
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { get; set; }
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
        public string? role { get; set; }
        public string? image { get; set; }
    }
}
