using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("teachers")]
    public class Teachers
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int teacher_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        [ForeignKey("teacher_id")]
        public ICollection<Lessons> Lekcje { get; set; }

        [ForeignKey("teacher_id")]

        public ICollection<Grades> Oceny { get; set; }

        [ForeignKey("teacher_id")]

        public ICollection<Warnings> Uwagi { get; set; }

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