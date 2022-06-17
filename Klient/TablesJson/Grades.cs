using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{ 
    public class Grades
    {
        public Guid grade_id { get; set; }
        public string grade { get; set; }
        public int weight { get; set; }
        public DateTime date { get; set; }
        public string desc { get; set; } 
        public Guid student_id{ get; set; }
        public Students Student{ get; set; }
        public Guid teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public Guid subject_id { get; set; }
        public Subjects Subjects { get; set; }

        public string Ocena
        {
            get
            {
                return "Ocena: " + grade + " Waga:" + student_id.ToString();  
            }
        }

        public override string ToString()
        {
            return Ocena;
        }
    }
}
