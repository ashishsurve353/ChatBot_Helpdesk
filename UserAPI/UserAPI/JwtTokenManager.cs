using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.UserModel;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace UserAPI
{
    public class JwtTokenManager : IJwtTokenManger
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;
        public JwtTokenManager(IConfiguration configuration, UserDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        

        public String Authenticate(string UserName, string Password)
        {
            Username user_object = _context.Usernames.FirstOrDefault(i => i.UserName1 == UserName);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(user_object));
            if (user_object == null)
            {
                return null;
            }
            else if (user_object.UserPassword == Password)
            {
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, UserName)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
                return null;

        }
        public String Generate(string UserName, string Password)
        {
            
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, UserName)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

        }


    }
}