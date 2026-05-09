using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DistSysAcwServer.Middleware;
using DistSysAcwServer.Models;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;


namespace DistSysAcwServer.Auth
{
    /// <summary>
    /// Authenticates clients by API Key
    /// </summary>
    public class CustomAuthenticationHandlerMiddleware
        : AuthenticationHandler<AuthenticationSchemeOptions>, IAuthenticationHandler
    {
        private Models.UserContext DbContext { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        private SharedError Error { get; set; }

        public CustomAuthenticationHandlerMiddleware(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            Models.UserContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            SharedError error)
            : base(options, logger, encoder)
        {
            DbContext = dbContext;
            HttpContextAccessor = httpContextAccessor;
            Error = error;
        }

        /// <summary>
        /// Authenticates the client by API Key
        /// </summary>
        /// now works authentication code (TASK 5 DONE)
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            #region Task5
            // TODO:  Using the ‘ApiKey’ header, authenticate (or do not authenticate) the client
            #endregion

            if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyValues))
            {
                return Task.FromResult(AuthenticateResult.Fail("No ApiKey header found"));
            }


            var dbAccess = new DistSysAcwServer.DataAccess.UserDatabaseAccess(DbContext);
            
                string? apiKey = apiKeyValues.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Error.StatusCode = StatusCodes.Status401Unauthorized;
                Error.Message = "Unauthorized. Check ApiKey in Header is correct.";
                return Task.FromResult(AuthenticateResult.Fail("Invalid ApiKey"));
            }
            User user = dbAccess.GetUserByApiKey(apiKey);

            if (user == null)
            {
                Error.StatusCode = StatusCodes.Status401Unauthorized;
                Error.Message = "Unauthorized. Check ApiKey in Header is correct.";
                return Task.FromResult(AuthenticateResult.Fail("Invalid ApiKey"));
            }

            var claims = new[]
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // Step 4: Build identity, principal and ticket
            var identity = new ClaimsIdentity(claims, "ApiKey");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }


        // now returns the correct 401 message code
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Error.StatusCode = StatusCodes.Status401Unauthorized;
            Error.Message = "Unauthorized. Check ApiKey in Header is correct.";
            return Task.CompletedTask;
        }
    }
}