using System;

namespace Dziennik_Szkolny
{

    /// <summary>
    /// Klasa przechowująca kolekcje obiektów do deserializacji otrzymanych danych
    /// </summary>

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
