using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("classes")]
    public class Student
    {
        //[Key]
        //[Column("UsosId")]
        //public uint Id { get; set; }

        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string class_id { get; set; }

        public string startyear { get; set; }

        public string course { get; set; }

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
