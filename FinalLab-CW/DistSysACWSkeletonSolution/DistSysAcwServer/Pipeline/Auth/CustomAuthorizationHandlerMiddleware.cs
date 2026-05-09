using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DistSysAcwServer.Middleware;
using DistSysAcwServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace DistSysAcwServer.Auth
{
    /// <summary>
    /// Authorises clients by role
    /// </summary>
    public class CustomAuthorizationHandlerMiddleware : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        private SharedError Error { get; set; }

        public CustomAuthorizationHandlerMiddleware(IHttpContextAccessor httpContextAccessor, SharedError error)
        {
            HttpContextAccessor = httpContextAccessor;
            Error = error;
        }

        /// <summary>
        /// Handles success or failure of the requirement for the user to be in a specific role
        /// </summary>
        /// <param name="context">Information used to decide on authorisation</param>
        /// <param name="requirement">Authorisation requirements</param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            bool isAllowed = requirement.AllowedRoles.Any(role => context.User.IsInRole(role));

            if (isAllowed)
            {
                context.Succeed(requirement);
            }
            else
            {
                bool adminOnly = requirement.AllowedRoles.Count() == 1 && requirement.AllowedRoles.Contains("Admin");

                if (adminOnly)
                {
                    Error.StatusCode = StatusCodes.Status403Forbidden;
                    Error.Message = "Forbidden. Admin access only.";
                }

                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}