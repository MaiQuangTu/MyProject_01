using Microsoft.AspNetCore.Identity;
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
    public class StudentSevices : IStudent
    {
        private readonly ApplicationDbContext _context;

        public StudentSevices(ApplicationDbContext db)
        {
            _context = db;
        }
        public PagedResult<StudentResult> GetStudent(Pageable pageable)
        {
            PagedResult<StudentResult> result = new PagedResult<StudentResult>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Student.Count(p => p.IsDelete != true);
            if (result.Total > 0)
            {
                // filter
                var lstStudent = _context.Student.Where(x => x.IsDelete != true).OrderBy(x => x.Name).Skip(skipRow).Take(pageable.Size).ToList();
                var lstStudentId = (from i in lstStudent
                                    select i.Id).ToList();
                List<StudentResult> StudentResults = new List<StudentResult>();
                foreach (var student in lstStudent)
                {
                    var StudentView = new StudentResult()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        gender = student.gender,
                        DateOfBirth = student.DateOfBirth,
                        express = student.express,
                        Point = student.Point
                    };
                    StudentResults.Add(StudentView);
                }
                result.Data = StudentResults;
            }

            return result;
        }

        public PagedResult<StudentResult> GetStudentByName(string name, Pageable pageable)
        {
            PagedResult<StudentResult> result = new PagedResult<StudentResult>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Student.Count(p => p.IsDelete != true);
            if (result.Total > 0)
            {
                // filter
                var lstStudentByName = _context.Student.Where(x => x.Name.IndexOf(name, System.StringComparison.OrdinalIgnoreCase) >= 0 && x.IsDelete != true).OrderBy(x => x.Name).Skip(skipRow).Take(pageable.Size).ToList();
                var lstContactId = (from i in lstStudentByName
                                    select i.Id).ToList();
                List<StudentResult> StudentResults = new List<StudentResult>();
                
                foreach (var student in lstStudentByName)
                {
                    var StudentView = new StudentResult()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        gender = student.gender,
                        DateOfBirth = student.DateOfBirth,
                        express = student.express,
                        Point = student.Point
                    };
                    StudentResults.Add(StudentView);
                }
                result.Data = StudentResults;
            }
            return result;
        }

        public void InsertStudent(StudentInputInsert model, int userId)
        {
            var student = new Student();
            student.Name = model.Name;
            student.gender = model.gender;
            student.DateOfBirth = model.DateOfBirth;
            student.express = model.express;
            student.IsDelete = model.IsDelete;


            _context.Student.Add(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(StudentInputUpdate model, int userId)
        {
            var student = _context.Student.Where(x => x.Id == model.Id && x.IsDelete != true).FirstOrDefault();
            if (student != null)
            {
                student.Name = model.Name;
                student.gender = model.gender;
                student.DateOfBirth = model.DateOfBirth;
                student.express = model.express;
                student.IsDelete = model.IsDelete;

                student.ModifyUser = userId;
                _context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            var objStudent = _context.Student.Where(x => x.Id == Id && x.IsDelete != true).FirstOrDefault();

            if (objStudent != null)
            {
                objStudent.IsDelete = true;
                _context.SaveChanges();
            }
        }

    }
}

