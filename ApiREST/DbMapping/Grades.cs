using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Grades")]
    public class Grades
    {
        [Key]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Grade_id { get; set; }
        public string Grade { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
        public string Desc { get; set; } 
        public Guid Student_id { get; set; }
        public Students Student{ get; set; }
        protected Guid Teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        [ForeignKey("Subjects")]
        public Guid Subject_id { get; set; }
        public Subjects Subjects { get; set; }

        public Grades(string Grade, int Weight, string Desc, Guid Student_id, Guid Subject_id, Guid Teacher_id)
        {
            this.Grade = Grade;
            this.Weight = Weight;
            this.Desc = Desc;
            this.Student_id = Student_id;
            this.Subject_id = Subject_id;
            this.Teacher_id = Teacher_id;
            this.Date = DateTime.Now;
        }

        public string Ocena
        {
            get
            {
                return "Ocena: " + Grade + " Waga:" + Weight.ToString();  
            }
        }

        public override string ToString()
        {
            return Ocena;
        }
    }
}
