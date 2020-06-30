using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ModeInput
{
    public class StudentInputUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string address { get; set; }
        public string Point { get; set; }
        public bool? IsDelete { get; set; }
    }
}
