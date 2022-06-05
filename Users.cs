using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("users")]
    public class Users
    {
        //[Key]
        //[Column("UsosId")]
        //public uint Id { get; set; }

        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int user_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }
        public int class_id { get; set; }

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