﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik_Szkolny
{
    [Table("users")]
    public class Users
    {

        [Key]
        [Required]
        [Index(IsUnique = true)]
        public int user_id { get; set; }

        [MinLength(2), MaxLength(20)]
        public string name { get; set; }

        [MinLength(2), MaxLength(40)]
        public string surname { get; set; }
        public string email { get; set; }
        public string passwd { get; set; }

        public int class_id { get; set; }
        public Classes Classes { get; set; }

        [ForeignKey("student_id")]
        public ICollection<Attendance> Obecnosci { get; set; }
        public ICollection<Grades> Oceny { get; set; }
        public ICollection<Warnings> Uwagi { get; set; }
        public ICollection<Parent_user> Rodzice_ucznia { get; set; }

        [NotMapped]
        public string Dane
        {
            get
            {
                return name + " " + surname;
            }
        }

        public override string ToString()
        {
            return Dane;
        }
    }
}