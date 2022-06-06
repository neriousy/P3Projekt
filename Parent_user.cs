using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("parent_user")]
    public class Parent_user
    {
        [Key]
        public int user_id { get; set; }
        [Key]
        public int parent_id { get; set; }

       

    }
}
