using System.Collections.Generic;
using System.Linq;
using DistSysAcwServer.DataAccess;
using DistSysAcwServer.Middleware;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DistSysAcwServer.Controllers
{
    public class TalkbackController : BaseController
    {
        private readonly UserDatabaseAccess _dbAccess;

        /// <summary>
        /// Constructs a TalkBack controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public TalkbackController(Models.UserContext dbcontext, SharedError error) : base(dbcontext, error)
        {
            _dbAccess = new UserDatabaseAccess(dbcontext);
        }

        // client can send a request, the server process it and then returns response (TASK1 DONE)
        #region TASK1
        //    TODO: add api/talkback/hello response
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Hello World");
        }
        #endregion

        #region TASK1

        [HttpGet]
        public IActionResult Sort([FromQuery] int[] integers)
        {
            if (integers == null || integers.Length == 0)
            {
                return Ok(System.Array.Empty<int>());
            }

            string? apiKey = Request.Headers.TryGetValue("ApiKey", out var apiKeyValues)
                ? apiKeyValues.FirstOrDefault()
                : null;

            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                _dbAccess.AddLog(apiKey, "User requested /User/RemoveUser");
                _dbAccess.AddLog(apiKey, "User requested /User/ChangeRole");
            }

            return Ok(integers.OrderBy(value => value).ToArray());
        }
        #endregion
    }
}