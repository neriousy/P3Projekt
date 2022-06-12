using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("students", Schema="public")]
    public class Students
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid student_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        public Guid class_id { get; set; }
        public Classes Classes { get; set; }

        [ForeignKey("student_id")]
        public ICollection<Attendance> Obecnosci { get; set; }
        public ICollection<Grades> Oceny { get; set; }
        public ICollection<Warnings> Uwagi { get; set; }
        public ICollection<Parents_students> Rodzice_ucznia { get; set; }

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