using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    public class Parents_students
    {
        public Guid Student_id { get; set; }
        public Students Student { get; set; }
        public Guid Parent_id { get; set; }
        public Parents Parent { get; set; }

    }
}
