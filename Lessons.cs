using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dziennik_Szkolny
{

    [Table("lessons")]
    public class Lessons
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string lesson_id { get; set; }
        public string subject_id { get; set; }
        public string class_id { get; set; }
        public string teacher_id { get; set; }
        public DateTime start_time { get; set; }


    }
}
