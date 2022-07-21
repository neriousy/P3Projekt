using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    public class Parents_students
    {
        public Guid student_id { get; set; }
        public Students student { get; set; }
        public Guid parent_id { get; set; }
        public Parents parent { get; set; }

    }
}
