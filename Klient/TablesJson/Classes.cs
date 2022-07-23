using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa przechowująca kolekcje obiektów do deserializacji otrzymanych danych
    /// </summary>

    public class Classes
    {
        public Guid Class_id { get; set; }
        public int StartYear { get; set; }
        public string Course { get; set; }

        public ICollection<Students> Students { get; set; }
        public ICollection<Lessons> Lessons { get; set; }

        public ICollection<Lesson_plan> Lesson_Plan { get; set; }
        public string Opis
        {
            get
            {
                return Class_id + " " + StartYear + " " + Course;
            }
        }

        public override string ToString()
        {
            return Opis;
        }
    }
}
