using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.HandleException;
using MyProject.Models.ModeInput;
using MyProject.Controllers;
using MyProject.Models.ViewModels;
using MyProject.Services;
using MyProject.Services.Common;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly string[] PermittedExtensions = { ".jpg", ".png" };
        private readonly TeacherSevices _TeacherService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TeacherController(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _TeacherService = new TeacherSevices(db);
            _userManager = userManager;
        }

        //[HttpGet]
        //[SwaggerOperation(Summary = "Get list all Teacher")]
        //public PagedResult<TeacherResult> GetTeacher([FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        //{
        //    Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
        //    return _TeacherService.GetTeacher(pageable);
        //}
        
        [HttpGet("{name}")]
        [SwaggerOperation(Summary = "Get list Teacher by name")]
        public PagedResult<TeacherResult> GetContactByNamePagination(string name, [FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        {
            Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
            return _TeacherService.GetTeacherByName(name, pageable);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create New Teacher")]
        public int InsertNote([FromForm] TeacherInputInsert model)
        {
            if (!ModelState.IsValid)
                throw new IllegalArgumentException("Input invalid!");

            // Check AttachFile
            var ext = Path.GetExtension(model.AttachFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !PermittedExtensions.Contains(ext))
            {
                throw new BadRequestException("AttachFile isn't allow!");
            }

            var userName = GetUserNameToTokenJWT();
            var userId = _userManager.Users.Where(x => x.UserName.ToLower().Equals(userName.ToLower())).Select(x => x.UserId).FirstOrDefault();

            var result = _TeacherService.InsertTeacher(model, userId);
            return result;
        }

        private string GetUserNameToTokenJWT()
        {
            try
            {
                var tokenUser = Request.Headers["Authorization"];

                if (String.IsNullOrEmpty(tokenUser))
                {
                    return "";
                }
                var handler = new JwtSecurityTokenHandler();
                var objToken = handler.ReadJwtToken(tokenUser.ToString().Replace("Bearer ", "")).Claims.ToList();

                var userName = objToken[0].Value;

                return userName;
            }
            catch
            {
                return "";
            }
        }
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Teacher by id")]
        public void Delete(int id)
        {
            _TeacherService.Delete(id);
        }
    }
}
