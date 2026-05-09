//TASK 9 - this is for using SHA1 and then SHA256

using DistSysAcwServer.Models;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DistSysAcwServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]

    public class ProtectedController : BaseController
    {
        public ProtectedController(UserContext dbcontext, SharedError error) : base(dbcontext, error)
        {

        }

        [HttpGet("Hello")]
        public IActionResult Hello()
        {
            string? username = User.FindFirstValue(ClaimTypes.Name);
            return Ok($"Hello {username}");
        }

        [HttpGet("SHA1")]
        public IActionResult SHA1Hash([FromQuery] string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Bad Request");
            }

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            using SHA1 sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(messageBytes);

            string hashString = Convert.ToHexString(hashBytes);

            return Ok(hashString);

        }

        [HttpGet("SHA256")]
        public IActionResult SHA256Hash([FromQuery] string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Bad Request");
            }

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(messageBytes);

            string hashString = Convert.ToHexString(hashBytes);

            return Ok(hashString);


        }
    }
}