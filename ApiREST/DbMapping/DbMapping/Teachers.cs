using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("teachers")]
    public class Teachers
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public Guid teacher_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        [ForeignKey("teacher_id")]
        public ICollection<Lessons> Lessons { get; set; }
        [ForeignKey("teacher_id")]
        public ICollection<Grades> Grades { get; set; }
        [ForeignKey("teacher_id")]
        public ICollection<Warnings> Warnings { get; set; }

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