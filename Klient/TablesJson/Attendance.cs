using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa przechowująca kolekcje obiektów do deserializacji otrzymanych danych
    /// </summary>

    public class Attendance
    {
        public Guid Attendance_id { get; set; }
        public DateTime Date { get; set; }
        public string IsPresent { get; set; }
        public string IsLate { get; set; }
        public Guid Student_id { get; set; }
        public Students Users { get; set; }
        public Guid Lesson_id { get; set; }
        public Lessons Lessons { get; set; }

        public string Obecnosc
        {
            get
            {
                return Date + " " + IsPresent == "1" ? "obecny" : "nieobecny";
            }
        }
        public override string ToString()
        {
            return Obecnosc;
        }

    }
}
