using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Subjects")]
    public class Subjects
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        protected Guid Subject_id { get; set; }
        public string Subjectname { get; set; }

        [ForeignKey("Subject_id")]
        protected ICollection<Lessons> Lessons { get; set; }
        protected ICollection<Grades> Grades { get; set; }

        [ForeignKey("Subject_id")]
        public Lesson_plan Lesson_plan { get; set; }

    }
}
