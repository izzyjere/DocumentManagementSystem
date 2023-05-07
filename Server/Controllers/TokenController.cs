using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RTSADocs.Server.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        [HttpPost]          
        public async Task<IActionResult> GenerateToken()
        {
            var result = await HttpContext.AuthenticateAsync(NegotiateDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("I122EDJKSNAMNADVHGDSWDSW_32323288Y38XBZX");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(result.Principal.Identity),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
    }

}
