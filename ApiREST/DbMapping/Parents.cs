using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Parents")]
    public class Parents
    {
        [Key]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Parent_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }

        [ForeignKey("Parent_id")]
        public ICollection<Parents_students> Parents_students { get; set; }


        [NotMapped]
        public string Dane
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public override string ToString()
        {
            return Dane;
        }
    }
}