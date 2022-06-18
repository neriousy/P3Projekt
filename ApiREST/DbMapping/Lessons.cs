using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiREST
{

    [Table("Lessons")]
    public class Lessons
    {
        [Key]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Lesson_id { get; set; }
        public Guid Subject_id { get; set; }
        public Subjects Subjects { get; set; }
        public Guid Class_id { get; set; }
        public Classes Classes{ get; set; }
        public Guid Teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public DateTime Start_time { get; set; }
        public string Topic { get; set; }

        [ForeignKey("Lesson_id")]
        public ICollection<Attendance> Attendances { get; set; }

        public Lessons(Guid Subject_id, Guid Class_id, Guid Teacher_id, string Topic)
        {
            this.Subject_id = Subject_id;
            this.Class_id = Class_id;
            this.Teacher_id = Teacher_id;
            this.Topic = Topic;
        }

    }
}
