using System;
using System.Collections.Generic;
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
        public int lesson_id { get; set; }
        public int subject_id { get; set; }
        public Subjects Subjects { get; set; }
        public int class_id { get; set; }
        public Classes Classes{ get; set; }
        public int teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public DateTime start_time { get; set; }

        [ForeignKey("lesson_id")]
        public ICollection<Attendance> Obecnosci { get; set; }

    }
}
