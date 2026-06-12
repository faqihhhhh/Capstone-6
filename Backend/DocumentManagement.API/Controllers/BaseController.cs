using System.IdentityModel.Tokens.Jwt;
using DocumentManagement.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DocumentManagement.API.Controllers
{
    public class BaseController : ControllerBase
    {
        internal string Email
        {
            get
            {
                Request.Headers.TryGetValue("Authorization", out var token);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    token = token.ToString().Replace("Bearer", "").Trim();
                    var handler = new JwtSecurityTokenHandler();
                    var tokeObject = handler.ReadToken(token) as JwtSecurityToken;
                    return tokeObject.Claims.FirstOrDefault(c => c.Type == "Email" || c.Type == "email")?.Value ?? tokeObject.Subject;
                }
                return "";
            }
        }

        public IActionResult GenerateResponse<T>(ServiceResponse<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode((int)result.StatusCode, result.Errors);
        }

    }
}