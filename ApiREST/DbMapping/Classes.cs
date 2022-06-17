using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Classes")]
    public class Classes
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Class_id { get; set; }
        public int StartYear { get; set; }
        public string Course { get; set; }

        [ForeignKey("Class_id")]
        public ICollection<Students> Students { get; set; }
        public ICollection<Lessons> Lessons { get; set; }
        
        [ForeignKey("Class_id")]
        public ICollection<Lesson_plan> Lesson_Plan { get; set; }

        [NotMapped]
        public string Opis
        {
            get
            {
                return Class_id + " " + StartYear + " " + Course;
            }
        }

        public override string ToString()
        {
            return Opis;
        }
    }
}
