using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    public class Classes
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public Guid Class_id { get; set; }
        public string StartYear { get; set; }
        public string Course { get; set; }

        [ForeignKey("class_id")]
        public ICollection<Students> Students { get; set; }
        public ICollection<Lessons> Lessons { get; set; }

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
