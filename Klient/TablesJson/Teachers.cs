using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa przechowująca kolekcje obiektów do deserializacji otrzymanych danych
    /// </summary>

    public class Teachers
    {
        public Guid Teacher_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public ICollection<Lessons> Lessons { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Warnings { get; set; }
        public string Dane
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public override string ToString()
        {
            return Dane;
        }
    }
}