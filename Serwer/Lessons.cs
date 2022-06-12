using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Serwer
{

    [Table("lessons", Schema="public")]
    public class Lessons
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid lesson_id { get; set; }
        public Guid subject_id { get; set; }
        public Subjects Subjects { get; set; }
        public Guid class_id { get; set; }
        public Classes Classes{ get; set; }
        public Guid teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public DateTime start_time { get; set; }

        [ForeignKey("lesson_id")]
        public ICollection<Attendance> Obecnosci { get; set; }

    }
}
