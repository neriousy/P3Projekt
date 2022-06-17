using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("subjects")]
    public class Subjects
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public Guid Subject_id { get; set; }
        public string Subjectname { get; set; }

        public ICollection<Lessons> Lessons { get; set; }
        public ICollection<Grades> Grades { get; set; }

        [ForeignKey("Subject_id")]
        public Lesson_plan Lesson_plan { get; set; }

    }
}
