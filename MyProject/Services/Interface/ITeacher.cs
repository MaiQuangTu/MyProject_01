using MyProject.Models.Dto;
using MyProject.Models.ModeInput;
using MyProject.Models.ModelResult;
using MyProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services.Interface
{
    interface ITeacher
    {
        PagedResult<Teacher> GetTeacher(Pageable pageable);
        PagedResult<TeacherResult> GetTeacherByName(string name, Pageable pageable);

        public int InsertTeacher(TeacherInputInsert model, int userId);
        public int UpdateTeacher(TeacherInputUpdate model, int userId);
    }
}
