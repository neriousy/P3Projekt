using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Teachers")]
    public class Teachers
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Teacher_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }

        [ForeignKey("Teacher_id")]
        public ICollection<Lessons> Lessons { get; set; }
        [ForeignKey("Teacher_id")]
        public ICollection<Grades> Grades { get; set; }
        [ForeignKey("Teacher_id")]
        public ICollection<Warnings> Warnings { get; set; }

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