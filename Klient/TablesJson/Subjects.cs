using System;
using System.Collections.Generic;

namespace Dziennik_Szkolny
{
    public class Subjects
    {
        public Guid Subject_id { get; set; }
        public string Subjectname { get; set; }

        public ICollection<Lessons> Lessons { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public Lesson_plan Lesson_plan { get; set; }

    }
}
