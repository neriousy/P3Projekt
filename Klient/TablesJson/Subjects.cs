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
        public Guid subject_id { get; set; }
        public string subjectname { get; set; }

        [ForeignKey("subject_id")]
        public ICollection<Lessons> Lekcje { get; set; }
        public ICollection<Grades> Oceny { get; set; }

    }
}
