using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("attendance", Schema="public")]
    public class Attendance
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid attendance_id { get; set; }
        public DateTime date { get; set; }
        public string attendance { get; set; }
        public string islate { get; set; }

        public Guid student_id { get; set; }
        public Students Users { get; set; }

        public Guid lesson_id { get; set; }
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
