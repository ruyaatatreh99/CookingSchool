using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking.Model
{
    [Table("Exam")]
    public class Exam
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public string? title { get; set; }
        [Required]
        public string? description { get; set; }
        [Required]
        [NotMapped]
        public IFormFile? photo { get; set; }
    }
}
