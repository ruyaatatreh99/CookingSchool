using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Cooking.Model
{
    [Table("Request")]
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public string ?StudentNaame { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [Required]
        public int ClassId { get; set; }
    }
}
