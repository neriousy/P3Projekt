using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("grades")]
    public class Grades
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string grade_id { get; set; }
        public string grade { get; set; }
        public int weight { get; set; }
        public DateTime date { get; set; }
        public string desc { get; set; }
        public string student_id { get; set; }
        public string teacher_id { get; set; }
        public string subject_id { get; set; }

        public string Ocena
        {
            get
            {
                return "Ocena: " + grade + " Waga:" + weight.ToString();  
            }
        }

        public override string ToString()
        {
            return Ocena;
        }
    }
}
