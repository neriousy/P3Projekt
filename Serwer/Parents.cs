using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("parents")]
    public class Parents
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int parent_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        [ForeignKey("parent_id")]

        public ICollection<Parent_user> Rodzice_ucznia { get; set; }


        [NotMapped]
        public string Dane
        {
            get
            {
                return name + " " + surname;
            }
        }

        public override string ToString()
        {
            return Dane;
        }
    }
}