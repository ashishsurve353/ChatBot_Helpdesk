//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using UserAPI.UserModel;

//namespace UserAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsernamesController : ControllerBase
//    {
//        private readonly UserDBContext _context;

//        public UsernamesController(UserDBContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Usernames
//        [Authorize]
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Username>>> GetUsernames()
//        {
//            return await _context.Usernames.ToListAsync();
//        }

//        // GET: api/Usernames/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Username>> GetUsername(string id)
//        {
//            var username = await _context.Usernames.FindAsync(id);

//            if (username == null)
//            {
//                return NotFound();
//            }

//            return username;
//        }

//        // PUT: api/Usernames/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUsername(string id, Username username)
//        {
//            if (id != username.UserName1)
//            {
//                return BadRequest();
//            }

//            _context.Entry(username).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UsernameExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Usernames
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPost]
//        //public JsonResult PostUsername(Username username)
//        //{
//        //    Username user_object = _context.Usernames.FirstOrDefault(i => i.UserName1 == username.UserName1);
//        //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(user_object));
//        //    if (user_object == null)
//        //    {
//        //        return new JsonResult("Not found");
//        //    }
//        //    else if (user_object.UserPassword == username.UserPassword)
//        //    {
//        //        return new JsonResult("Found");
//        //    }
//        //    else
//        //    {
//        //        return new JsonResult("Not found");
//        //    }

//        // return CreatedAtAction("GetUsername", new { id = username.UserName1 }, username);
//        //}

//        // DELETE: api/Usernames/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUsername(string id)
//        {
//            var username = await _context.Usernames.FindAsync(id);
//            if (username == null)
//            {
//                return NotFound();
//            }

//            _context.Usernames.Remove(username);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UsernameExists(string id)
//        {
//            return _context.Usernames.Any(e => e.UserName1 == id);
//        }
//    }
//}
