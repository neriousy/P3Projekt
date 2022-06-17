using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiREST
{
    [Table("Warnings")]
    public class Warnings
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Warning_id { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public Guid Student_id { get; set; }
        public Students Users { get; set; }
        public Guid Teacher_id { get; set; }
        public Teachers Teachers { get; set; }
    }
}
