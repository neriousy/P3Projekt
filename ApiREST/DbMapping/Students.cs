using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;


namespace ApiREST
{
    [Table("Students")]
    public class Students
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Student_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }

        public Guid Class_id { get; set; }
        public Classes Classes { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        [ForeignKey("Student_id")]
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Warnings { get; set; }
        [ForeignKey("Student_id")]
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