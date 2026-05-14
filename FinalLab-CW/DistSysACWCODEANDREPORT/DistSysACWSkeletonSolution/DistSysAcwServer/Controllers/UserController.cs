using DistSysAcwServer.DataAccess;
using DistSysAcwServer.Models;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DistSysAcwServer.Models;

namespace DistSysAcwServer.Controllers
{
    // code for user controller (task 4) basically tries to do two actons: GET and POST
    // GET fetches a name from a URL and if username is real returns message if not returns message saying it can't find it.

    // POST is more so about the JSON files following correct formatting e.g., "UserOne" not {username: "UserOne"}
    // apparently needed to add this but it now works and has a unique key for the user yay!!!
    public class ChangeRoleRequest
    {
        public string? Username { get; set; }
        public string? Role { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
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

        //checking user roles and if they are valid  (error checking) or not (TASK 8)
        [HttpPost("ChangeRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRole([FromBody] ChangeRoleRequest? request) 
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest("NOT DONE: Username does not exist");
                }
            if (!_dbAccess.UserNameExists(request.Username))
                { return BadRequest("NOT DONE: Username does not exist"); 
                }
            
            if (request.Role != "User" && request.Role != "Admin")
                { return BadRequest("NOT DONE: Role does not exist"); 
                }

                bool changed = _dbAccess.ChangeUserRole(request.Username, request.Role);
            if (changed)
                { 
                    return Ok("DONE"); 
                }
                return BadRequest("NOT DONE: An error occured");
            
            }
        catch
            { return BadRequest("NOT DONE: An error occured");
            }
        }

        //removes user (Task 7 DONE)

        [HttpDelete("RemoveUser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult RemoveUser([FromQuery] string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Ok(false);
            }

            string? apiKey = Request.Headers["ApiKey"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return Ok(false);
            }

            User? requester = _dbAccess.GetUserByApiKey(apiKey);

            if (requester == null)
            {
                return Ok(false);
            }

            bool isDeletingSelf = requester.UserName == username;
            bool isAdmin = requester.Role == "Admin";

            if (!isDeletingSelf && !isAdmin)
            {
                return Ok(false);
            }

            bool deleted = _dbAccess.DeleteUserByUserName(username);

            return Ok(deleted);
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