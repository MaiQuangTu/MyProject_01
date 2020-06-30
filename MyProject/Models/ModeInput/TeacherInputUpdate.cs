using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ModeInput
{
    public class TeacherInputUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string address { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        [Required]
        public IFormFile AttachFile { get; set; }
        public int? ModifyUser { get; set; }
        public bool? IsDelete { get; set; }
    }
}
