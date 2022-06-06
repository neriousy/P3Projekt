using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("attendance")]
    public class Attendance
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int attendance_id { get; set; }
        public DateTime date { get; set; }
        public string attendance { get; set; }
        public string islate { get; set; }

        public int student_id { get; set; }
        public Students Users { get; set; }

        public int lesson_id { get; set; }
        public Lessons Lessons { get; set; }

        public string Obecnosc
        {
            get
            {
                return date + " " + attendance == "1" ? "obecny" : "nieobecny";
            }
        }
        public override string ToString()
        {
            return Obecnosc;
        }

    }
}
