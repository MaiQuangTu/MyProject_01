using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProject.Services.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.Dto
{
    public class Student:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set;}
        public string gender { get; set;}
        public DateTime DateOfBirth { get; set;}
        public string address { get; set;}
        public string Point { get; set;}
        public bool? IsDelete { get; set;}
        public string UserName { get; set; }

    }
}
