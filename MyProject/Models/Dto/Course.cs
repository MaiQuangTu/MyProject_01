﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Dto
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public DateTime TimeStar { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeToRegister { get; set; }
        public DateTime TimeStarCourse { get; set; }
        public DateTime Total { get; set; }
        public int MaxStudent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
