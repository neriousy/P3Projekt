﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwer
{
    [Table("waranings")]
    public class Warnings
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int warning_id { get; set; }
        public string desc { get; set; }
        public DateTime date { get; set; }
        public int student_id { get; set; }
        public Students Users { get; set; }
        public int teacher_id { get; set; }
        public Teachers Teachers { get; set; }
    }
}