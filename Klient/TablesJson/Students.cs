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
        public Guid student_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        public Guid class_id { get; set; }
        public Classes Classes { get; set; }

        public ICollection<Attendance> Obecnosci { get; set; }
       
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Warnings> Uwagi { get; set; }
        public ICollection<Parents_students> Parents_students { get; set; }
        public string Dane
        {
            get
            {
                return name + " " + surname;
            }
        }

        public override string ToString()
        {
            return Dane;
        }
    }
}