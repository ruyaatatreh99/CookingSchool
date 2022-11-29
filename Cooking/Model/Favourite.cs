using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooking.Model
{
    [Table("Favourite")]
    public class Favourite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id{ get; set; }
        [Required]
        public int userid { get; set; }
        [Required]
        public int mealid { get; set; }
    }
}
