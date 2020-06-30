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
    interface ICourse
    {
        PagedResult<Course> GetCourse(Pageable pageable);
        Course GetCourseByCode(string Code);

        public void InsertCourse(CourseInputInsert model, int teacherId);
        public void UpdateCourse(CourseInputUpdate model, int userId);
        //
    }
}
