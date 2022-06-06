using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("waranings")]
    public class Warnings
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int warning_id { get; set; }
        public string desc { get; set; }
        public DateTime date { get; set; }
        public int student_id { get; set; }
        public int teacher_id { get; set; }
    }
}
