using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.HandleException;
using MyProject.Models.Dto;
using MyProject.Models.ModeInput;
using MyProject.Models.ModelResult;
using MyProject.Models.ViewModels;
using MyProject.Services;
using MyProject.Services.Common;
using MyProject.Services.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _CourseService;
        public CoursesController(ApplicationDbContext db)
        {
            _CourseService = new CourseService(db);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list Course")]
        public PagedResult<Course> GetCourse([FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        {
            Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
            return _CourseService.GetCourse(pageable);
        }

        //[HttpGet("name/")]
        //[SwaggerOperation(Summary = "Get Course With Filter")]
        //public PagedResult<CourseFilterResult> CourseFilter(string userName, [FromQuery(Name = "filterBy")] string filterBy, [FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        //{
        //    Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
        //    return _CourseService.CourseFilter(userName, filterBy, pageable);
        //}


        [HttpGet("Code")]
        [SwaggerOperation(Summary = "Get Course By Code")]
        public Course GetCourseByCode(string code)
        {
            return _CourseService.GetCourseByCode(code);
        }
    }
}
