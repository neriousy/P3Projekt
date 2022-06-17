﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiREST
{

    [Table("Lessons")]
    public class Lessons
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Lesson_id { get; set; }
        public Guid Subject_id { get; set; }
        public Subjects Subjects { get; set; }
        public Guid Class_id { get; set; }
        public Classes Classes{ get; set; }
        public Guid Teacher_id { get; set; }
        public Teachers Teachers { get; set; }
        public DateTime Start_time { get; set; }

        [ForeignKey("Lesson_id")]
        public ICollection<Attendance> Attendances { get; set; }

    }
}
