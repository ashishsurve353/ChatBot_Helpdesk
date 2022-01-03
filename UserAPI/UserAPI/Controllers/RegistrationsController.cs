using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserAPI.UserModel;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationDBContext _context;
        private readonly UserDBContext _context2;
        private readonly IJwtTokenManger _tokenManger;

        public RegistrationsController(IJwtTokenManger jwtTokenManger,RegistrationDBContext context, UserDBContext context2)
        {
            _context = context;
            _context2 = context2;
            _tokenManger = jwtTokenManger;
        }

        // GET: api/Registrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationFeed>>> GetRegistrations()
        {
            return await _context.Registrations.ToListAsync();
        }

        //// GET: api/Registrations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Registration>> GetRegistration(string id)
        //{
        //    var registration = await _context.Registrations.FindAsync(id);

        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    return registration;
        //}

        // PUT: api/Registrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRegistration(string id, Registration registration)
        //{
        //    if (id != registration.UserName1)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(registration).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegistrationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Registrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName(nameof(PostRegistration))]
        public IActionResult PostRegistration(RegistrationFeed registration)
        {
            Token tokenobj = new Token();
            var token = _tokenManger.Generate(registration.UserName1, registration.UserPassword);
            //Registration obj = new RegistrationFeed();
            _context.Registrations.Add(registration);
            try
            {
                 _context.SaveChanges();
                Username user_object = _context2.Usernames.FirstOrDefault(i => i.UserName1 == registration.UserName1);
                user_object.Key = token;
                //Console.WriteLine("helloooooooo");
                _context2.SaveChanges();
                tokenobj.AccessToken = token;
                tokenobj.Error = null;
            }
            catch (DbUpdateException)
            {
                if (RegistrationExists(registration.UserName1))
                {
                    return Conflict();
                }
                return BadRequest();
            }
            //return Ok(token);
            //return Ok(JsonConvert.SerializeObject(token));

            tokenobj.Role = "User";
            tokenobj.Name = registration.UserName1;
            return Ok(JsonConvert.SerializeObject(tokenobj));

            // return CreatedAtAction("GetRegistration", new { id = registration.UserName1 }, registration);
        }

        // DELETE: api/Registrations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRegistration(string id)
        //{
        //    var registration = await _context.Registrations.FindAsync(id);
        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Registrations.Remove(registration);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool RegistrationExists(string id)
        {
            return _context.Registrations.Any(e => e.UserName1 == id);
        }
    }
}
