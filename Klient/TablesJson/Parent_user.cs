using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("parents_students")]
    public class Parents_students
    {
        [Key]
        public Guid student_id { get; set; }
        public Students Student { get; set; }
        [Key]
        public Guid parent_id { get; set; }
        public Parents Parent { get; set; }



    }
}
