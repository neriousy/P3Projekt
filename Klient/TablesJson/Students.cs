using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;


namespace Dziennik_Szkolny
{
    public class Students
    {
        public Guid Student_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public Guid Plass_id { get; set; }
        public Classes Classes { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Warnings { get; set; }
        public ICollection<Parents_students> Parents_students { get; set; }
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