using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.UserModel;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManger _tokenManger;
        private readonly UserDBContext _context;
        public TokenController(IJwtTokenManger jwtTokenManger, UserDBContext context)
        {
            _tokenManger = jwtTokenManger;
            _context = context;

        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Login user)
        {
            var token = _tokenManger.Authenticate(user.UserName1, user.UserPassword);
            Token tokenobj = new Token();
            if (string.IsNullOrEmpty(token))
            {
                tokenobj.AccessToken = null;
                tokenobj.Error = "unauthorized";
                tokenobj.Role = "null";
                tokenobj.Name = user.UserName1;
                return Ok(JsonConvert.SerializeObject(tokenobj));
            }
            Username user_object = _context.Usernames.FirstOrDefault(i => i.UserName1 == user.UserName1);
            user_object.Key = token;
            _context.SaveChanges();
            tokenobj.AccessToken = token;
            tokenobj.Error = "None";
            tokenobj.Role = user_object.UserRole;
            tokenobj.Name = user.UserName1;
            return Ok(JsonConvert.SerializeObject(tokenobj));

        }
    }
}
