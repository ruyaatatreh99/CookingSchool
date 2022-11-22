﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cooking.Model
{
    [Table("Mark")]
    public class Mark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Mark value is required")]
        public double? Markvalue { get; set; }
        [Required]
        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        [Required]
        [ForeignKey("Course")]
        public int? CourseID { get; set; }
    }
}
