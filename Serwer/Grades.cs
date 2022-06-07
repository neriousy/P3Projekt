using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("grades")]
    public class Grades
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int grade_id { get; set; }
        public string grade { get; set; }
        public int weight { get; set; }
        public DateTime date { get; set; }
        public string desc { get; set; }
        public int student_id { get; set; }
        public Students Users { get; set; }
        public int teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public int subject_id { get; set; }
        public Subjects Subjects { get; set; }

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
