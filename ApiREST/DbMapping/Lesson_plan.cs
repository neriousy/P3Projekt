using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiREST
{

    [Table("Lesson_plan")]
    public class Lesson_plan
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Lesson_id { get; set; }
        public Guid Subject_id { get; set; }
        public ICollection<Subjects> Subjects { get; set; }
        public Guid Class_id { get; set; }
        public Classes Class { get; set; }
        public Guid Teacher_id { get; set; }
        public Teachers Teacher { get; set; }
        public int Start_time { get; set; }
        public int Day { get; set; }

    }
}
