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
<<<<<<< HEAD
<<<<<<< HEAD
        public ICollection<Lessons> Lessons { get; set; }
        public ICollection<Grades> Grades { get; set; }
=======
=======
>>>>>>> 43642cb3c38e8ce6f67c16f0908586bc58be5c6f
        protected ICollection<Lessons> Lessons { get; set; }
        protected ICollection<Grades> Grades { get; set; }

        [ForeignKey("Subject_id")]
        public Lesson_plan Lesson_plan { get; set; }
<<<<<<< HEAD
>>>>>>> 43642cb3c38e8ce6f67c16f0908586bc58be5c6f
=======
>>>>>>> 43642cb3c38e8ce6f67c16f0908586bc58be5c6f

    }
}
