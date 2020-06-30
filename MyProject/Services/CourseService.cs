using MyProject.Data;
using MyProject.Models.Dto;
using MyProject.Models.ModeInput;
using MyProject.Models.ModelResult;
using MyProject.Models.ViewModels;
using MyProject.Services.Common;
using MyProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class CourseService : ICourse
    {
        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext db)
        {
            _context = db;
        }

        public PagedResult<CourseResult> FilterCourse(string userName, string FilterBy, Pageable pageable)
        {
            PagedResult<CourseResult> result = new PagedResult<CourseResult>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Course.Count();
            if(result.Total > 0)
            {
                List<CourseResult> CourseFilterResult = new List<CourseResult>();
                if(string.IsNullOrEmpty(FilterBy))
                {
                    CourseFilterResult = (from s in _context.Course
                                          join c in _context.StudentCourse on s.Id equals c.CourseId
                                          where s.Id == c.StudentId
                                          select new CourseResult()
                                          {
                                              Code = s.Code,
                                              Description=s.Description,
                                              Level = s.Level,
                                              TimeEnd = s.TimeEnd,
                                              TimeStar = s.TimeStar,
                                              TimeToRegister = s.TimeToRegister,
                                              TimeStarCourse = s.TimeStarCourse
                                          }
                                          ).ToList();
                }    
                else
                {
                    switch(FilterBy)
                    {
                        case "ChuaBatDau":
                            CourseFilterResult = (from s in _context.Course
                                                  where s.TimeStar > DateTime.Now
                                                  select new CourseResult
                                                  {
                                                      Code = s.Code,
                                                      Description = s.Description,
                                                      Level = s.Level,
                                                      TimeEnd = s.TimeEnd,
                                                      TimeStar = s.TimeStar,
                                                      TimeToRegister = s.TimeToRegister,
                                                      TimeStarCourse = s.TimeStarCourse
                                                  }
                                                  ).ToList();
                            break;
                        case "DangHoc":
                            CourseFilterResult = (from s in _context.Course
                                                  where s.TimeStar < DateTime.Now &&
                                                  s.TimeEnd > DateTime.Now
                                                  select new CourseResult
                                                  {
                                                      Code = s.Code,
                                                      Description = s.Description,
                                                      Level = s.Level,
                                                      TimeEnd = s.TimeEnd,
                                                      TimeStar = s.TimeStar,
                                                      TimeToRegister = s.TimeToRegister,
                                                      TimeStarCourse = s.TimeStarCourse
                                                  }).ToList();
                            break;
                        case "DaKetThuc":
                            CourseFilterResult = (from s in _context.Course
                                                  where s.TimeEnd < DateTime.Now
                                                  select new CourseResult
                                                  {
                                                      Code = s.Code,
                                                      Description = s.Description,
                                                      Level = s.Level,
                                                      TimeEnd = s.TimeEnd,
                                                      TimeStar = s.TimeStar,
                                                      TimeToRegister = s.TimeToRegister,
                                                      TimeStarCourse = s.TimeStarCourse
                                                  }
                                                  ).ToList();
                            break;

                    }    
                }
                var ResultEnd = CourseFilterResult.Skip(skipRow).Take(pageable.Size).ToList();
                result.Data = ResultEnd;
            }
            return result;
        }

        public PagedResult<Course> GetCourse(Pageable pageable)
        {
            PagedResult<Course> result = new PagedResult<Course>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Course.Count();
            if (result.Total > 0)
            {
                List<Course> entities = _context.Course.Skip(skipRow).Take(pageable.Size).ToList();
                result.Data = entities;
            }
            return result;
        }

        public Course GetCourseByCode(string _Code)
        {
            return _context.Course.SingleOrDefault(x => x.Code == _Code);
        }

        public void InsertCourse(CourseInputInsert model, int TeacherID)
        {
            var course = new Course();
            course.Code = model.Code;
            course.Description = model.Description;
            course.Level = model.Level;
            course.TimeStar = model.TimeStar;
            course.TimeEnd = model.TimeEnd;
            course.CreateDate = model.CreateDate;
            course.MaxStudent = model.MaxStudent;
            _context.Course.Add(course);
            _context.SaveChanges();
            //
            CourseTeacher courseTeacher = new CourseTeacher();
            courseTeacher.CourseId = course.Id;
            courseTeacher.TeacherId = TeacherID;
            _context.SaveChanges();
        }

        public void UpdateCourse(CourseInputUpdate model, int userId)
        {
            var course = _context.Course.SingleOrDefault(p => p.Id == model.Id);
            if (course != null)
            {
                course.Code = model.Code;
                course.Description = model.Description;
                course.Level = model.Level;
                course.TimeStar = model.TimeStar;
                course.TimeEnd = model.TimeEnd;
                course.MaxStudent = model.MaxStudent;
                _context.SaveChanges();
               
            }
        }
    }
}
