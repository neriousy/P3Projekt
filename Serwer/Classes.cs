using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("classes", Schema="public")]
    public class Classes
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid class_id { get; set; }
        public string startyear { get; set; }
        public string course { get; set; }

        [ForeignKey("class_id")]
        public ICollection<Students> Uzytkownicy { get; set; }
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
