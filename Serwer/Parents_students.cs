using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("parents_students", Schema="public")]
    public class Parents_students
    {
        [Key]
        public Guid student_id { get; set; }
        public Students Users { get; set; }
        [Key]
        public Guid parent_id { get; set; }
        public Parents Parents { get; set; }



    }
}
