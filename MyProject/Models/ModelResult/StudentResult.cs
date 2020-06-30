using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ModelResult
{
    public class StudentResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string address { get; set; }
        public string Point { get; set; }
        public int? CreateUser { get; set; }
        public int? ModifyUser { get; set; }
    }
}
