using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("grades", Schema="public")]
    public class Grades
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid grade_id { get; set; }
        public string grade { get; set; }
        public int weight { get; set; }
        public DateTime date { get; set; }
        public string desc { get; set; }
        public Guid student_id { get; set; }
        public Students Users { get; set; }
        public Guid teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public Guid subject_id { get; set; }
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
