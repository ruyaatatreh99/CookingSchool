using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cooking.Model
{
    [Table("StudentClass")]
    public class StudentClass
    {
        public int StudentID { get; set; }
        public Student ?Student { get; set; }
     
        public int ClassId { get; set; }
        public Class ?Class { get; set; }
    }
}
