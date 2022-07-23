using System;
using System.Collections.Generic;


namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa przechowująca kolekcje obiektów do deserializacji otrzymanych danych
    /// </summary>

    public class Students
    {
        public Guid student_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }
        public Guid class_id { get; set; }
        public Classes Classes { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Warnings { get; set; }
        public ICollection<Parents_students> Parents_students { get; set; }
        public string dane
        {
            get
            {
                return name + " " + surname;
            }
        }

        public override string ToString()
        {
            return dane;
        }
    }
}