using DistSysAcwServer.DataAccess;
using DistSysAcwServer.Models;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSysAcwServer.Controllers
{
    // code for user controller (task 4) basically tries to do two actons: GET and POST
    // GET fetches a name from a URL and if username is real returns message if not returns message saying it can't find it.

    // POST is more so about the JSON files following correct formatting e.g., "UserOne" not {username: "UserOne"}
    // apparently needed to add this but it now works and has a unique key for the user yay!!!

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //added now should authorize

    public class UserController : BaseController
    {
        private readonly UserDatabaseAccess _dbAccess;

        public UserController(UserContext dbcontext, SharedError error) : base(dbcontext, error)
        {
            _dbAccess = new UserDatabaseAccess(dbcontext);
        }
        //changed it to new? will it work
        [HttpGet("New")]
        public IActionResult GetNew([FromQuery] string? username)
        {
            if (!string.IsNullOrWhiteSpace(username) && _dbAccess.UserNameExists(username))
            {
                return Ok("True - User Does Exist! Did you mean to do a POST to create a new user?");
            }

            return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user?");
        }

        [HttpPost("new")]
        public IActionResult PostNew([FromBody] string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json");
            }

            if (_dbAccess.UserNameExists(username))
            {
                return StatusCode(403, "Oops. This username is already in use. Please try again with a new username.");
            }

            User user = _dbAccess.CreateUser(username);

            return Ok(user.ApiKey);
        }
    }
}