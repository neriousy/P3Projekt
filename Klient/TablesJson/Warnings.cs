using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("warnings")]
    public class Warnings
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public Guid warning_id { get; set; }
        public string desc { get; set; }
        public DateTime date { get; set; }
        public Guid student_id { get; set; }
        public Students Users { get; set; }
        public Guid teacher_id { get; set; }
        public Teachers Teachers { get; set; }
    }
}
