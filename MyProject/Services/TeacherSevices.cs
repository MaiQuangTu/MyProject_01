using Microsoft.AspNetCore.Http;
using MyProject.Data;
using MyProject.HandleException;
using MyProject.Models.Dto;
using MyProject.Models.ModeInput;
using MyProject.Models.ViewModels;
using MyProject.Services.Common;
using MyProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class TeacherSevices : ITeacher
    {
        private readonly ApplicationDbContext _context;

        public TeacherSevices(ApplicationDbContext db)
        {
            _context = db;
        }
        public PagedResult<Teacher> GetTeacher(Pageable pageable)
        {
            PagedResult<Teacher> result = new PagedResult<Teacher>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Teacher.Count();
            if (result.Total > 0)
            {
                List<Teacher> entities = _context.Teacher
                    .Skip(skipRow)
                    .Take(pageable.Size).ToList();
                result.Data = entities;
            }
            return result;
        }

        public PagedResult<TeacherResult> GetTeacherByName(string name, Pageable pageable)
        {
            PagedResult<TeacherResult> result = new PagedResult<TeacherResult>();
            result.Page = pageable.Page;
            result.Size = pageable.Size;
            int skipRow = PaginatorUtils.GetSkipRow(pageable.Page, pageable.Size);
            result.Total = _context.Student.Count(p => p.IsDelete != true);
            if (result.Total > 0)
            {
                // filter
                var lstTeacherByName = _context.Teacher.Where(x => x.Name.IndexOf(name, System.StringComparison.OrdinalIgnoreCase) >= 0 && x.IsDelete != true).OrderBy(x => x.Name).Skip(skipRow).Take(pageable.Size).ToList();
                var lstTeacherId = (from i in lstTeacherByName
                                    select i.Id).ToList();
                List<TeacherResult> TeacherResults = new List<TeacherResult>();

                foreach (var teacher in lstTeacherByName)
                {
                    var TeacherView = new TeacherResult()
                    {
                        Id = teacher.Id,
                        Name = teacher.Name,
                        gender = teacher.gender,
                        DateOfBirth = teacher.DateOfBirth,
                        address = teacher.address,
                        ImageName = teacher.ImageName,
                        ImagePath = teacher.ImagePath
                    };
                    TeacherResults.Add(TeacherView);
                }
                result.Data = TeacherResults;
            }
            return result;
        }

        public int InsertTeacher(TeacherInputInsert model, int userId)
        {
            try
            {
                string uniqueFileName = UploadedFile(model.AttachFile);

                var entity = new Teacher();

                entity.Name = model.Name;
                entity.gender = model.gender;
                entity.DateOfBirth = model.DateOfBirth;
                entity.address = model.address;
                entity.ImageName = uniqueFileName;

                entity.ImagePath = "/Image/" + uniqueFileName;
                _context.Teacher.Add(entity);

                return _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public int UpdateTeacher(TeacherInputUpdate model, int userId)
        {
            var contact = _context.Teacher.Where(x => x.Id == model.Id && x.IsDelete != true).FirstOrDefault();
            string uniqueFileName = UploadedFile(model.AttachFile);

                var entity = new Teacher();

                entity.Name = model.Name;
                entity.gender = model.gender;
                entity.DateOfBirth = model.DateOfBirth;
                entity.address = model.address;

                entity.ImageName = uniqueFileName;
                entity.ImagePath = "/Image/" + uniqueFileName;
                _context.Teacher.Add(entity);

                return _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var objTeacher = _context.Teacher.Where(x => x.Id == Id && x.IsDelete != true).FirstOrDefault();

            if (objTeacher != null)
            {
                objTeacher.IsDelete = true;
                _context.SaveChanges();
            }
        }
    }
}
