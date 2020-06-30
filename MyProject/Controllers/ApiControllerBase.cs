using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class ApiControllerBase: ControllerBase
    {
        [HttpGet]
        public string GetUserNameToTokenJWT()
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
