using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Dto
{
    public class CourseTeacher
    {
        [Key]
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
