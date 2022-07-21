using System;
using System.Collections.Generic;
namespace Dziennik_Szkolny
{
    public class Lessons
    {
        public Guid lesson_id { get; set; }
        public Guid subject_id { get; set; }
        public Subjects subjects { get; set; }
        public Guid class_id { get; set; }
        public Classes classes { get; set; }
        public Guid teacher_id { get; set; }
        public Teachers teachers { get; set; }
        public DateTime start_time { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

    }
}
