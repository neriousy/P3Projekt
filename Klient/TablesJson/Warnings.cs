using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    public class Warnings
    {
        public Guid warning_id { get; set; }
        public string desc { get; set; }
        public DateTime date { get; set; }
        public Guid student_id { get; set; }
        public Students users { get; set; }
        public Guid teacher_id { get; set; }
        public Teachers teachers { get; set; }
    }
}
