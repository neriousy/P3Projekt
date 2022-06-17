using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Parents_students")]
    public class Parents_students
    {
        [Key]
        public Guid Student_id { get; set; }
        public Students Student { get; set; }
        [Key]
        public Guid Parent_id { get; set; }
        public Parents Parent { get; set; }



    }
}
