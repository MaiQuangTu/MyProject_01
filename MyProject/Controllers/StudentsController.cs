using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Identity;
using MyProject.Models.ModeInput;
using System.IdentityModel.Tokens.Jwt;
using MyProject.Models.ViewModels;
using MyProject.Models.ModelResult;
using MyProject.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : ControllerBase
    {
        private readonly StudentSevices _studentcontext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _studentcontext = new StudentSevices(db);
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/Students
        [HttpGet]
        [SwaggerOperation(Summary = "Get list all Student")]
        public PagedResult<StudentResult> GetStudent([FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        {
            Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
            return _studentcontext.GetStudent(pageable);
        }

        // GET: api/Students/5
        [HttpGet("{Name}")]
        [SwaggerOperation(Summary = "Get Student By Name")]
        public PagedResult<StudentResult> GetStudentByName(string name, [FromQuery(Name = "page")] int? page, [FromQuery(Name = "size")] int? size)
        {
            Pageable pageable = new Pageable(PaginatorUtils.GetPageNumber(page), PaginatorUtils.GetPageSize(size));
            return _studentcontext.GetStudentByName(name, pageable);
        }

        // PUT: api/Students/
        [HttpPost("{id}")]

        [SwaggerOperation(Summary = "Insert Student")]
        public void InsertStudent(StudentInputInsert model)
        {
            var userName = GetUserNameToTokenJWT();
            var userId = _userManager.Users.Where(x => x.UserName.ToLower().Equals(userName.ToLower())).Select(x => x.UserId).FirstOrDefault();
            _studentcontext.InsertStudent(model,userId);
        }

        // POST: api/Students
        [HttpPut]
        public void UpdateStudent([FromBody] StudentInputUpdate model)
        {
            var userName = GetUserNameToTokenJWT();
            int userId = _userManager.Users.Where(x => x.UserName.ToLower().Equals(userName.ToLower())).Select(x => x.UserId).FirstOrDefault();
            _studentcontext.UpdateStudent(model, userId);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Students by id")]
        public void Delete(int id)
        {
            _studentcontext.Delete(id);
        }

        //private bool StudentExists(int id)
        //{
        //    return _studentcontext.Student.Any(e => e.Id == id);
        //}
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
    }
}
