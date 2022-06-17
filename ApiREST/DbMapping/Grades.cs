using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Grades")]
    public class Grades
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Grade_id { get; set; }
        public string Grade { get; set; }
        public int Weight { get; set; }
        public DateTime Date { get; set; }
        public string Desc { get; set; } 
        public Guid Student_id { get; set; }
        public Students Student{ get; set; }
        public Guid Teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        [ForeignKey("Subjects")]
        public Guid Subject_id { get; set; }
        public Subjects Subjects { get; set; }

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
