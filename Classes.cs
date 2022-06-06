using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("classes")]
    public class Classes
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int class_id { get; set; }
        public string startyear { get; set; }
        public string course { get; set; }

        [ForeignKey("class_id")]
        public ICollection<Users> Uzytkownicy { get; set; }
        public ICollection<Lessons> Lekcje { get; set; }

        [NotMapped]
        public string Opis
        {
            get
            {
                return class_id + " " + startyear + " " + course;
            }
        }

        public override string ToString()
        {
            return Opis;
        }
    }
}
