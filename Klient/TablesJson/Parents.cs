using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    public class Parents
    {
        public Guid Parent_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
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