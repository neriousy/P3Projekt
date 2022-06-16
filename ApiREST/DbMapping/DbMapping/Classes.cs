using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("classes")]
    public class Classes
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
<<<<<<<< HEAD:ApiREST/DbMapping/DbMapping/Classes.cs
========
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

>>>>>>>> 85f433c326d4d751bdc8a41be75b3ce4c943006e:ApiREST/DbMapping/Classes.cs
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
