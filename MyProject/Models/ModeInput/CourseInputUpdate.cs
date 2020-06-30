using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ModeInput
{
    public class CourseInputUpdate
    {
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
        public bool? IsDelete { get; set; }
        public int? CreateUser { get; set; }
        public int? ModifyUser { get; set; }
    }
}
