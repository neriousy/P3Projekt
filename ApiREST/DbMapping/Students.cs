using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;


namespace ApiREST
{
    [Table("students")]
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

        public ICollection<Attendance> Obecnosci { get; set; }
        [ForeignKey("student_id")]
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Uwagi { get; set; }
        [ForeignKey("student_id")]
        public ICollection<Parents_students> Parents_students { get; set; }

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