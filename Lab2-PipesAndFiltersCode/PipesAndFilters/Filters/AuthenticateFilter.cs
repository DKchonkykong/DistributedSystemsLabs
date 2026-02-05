using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Filters
{
    internal class AuthenticateFilter : IFilter
    {
        //run method checks for user header and if valid sets current user in server enviroment then message passes unchanged
        public IFilter Run(IFilter message)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));

            if (message.Headers != null && message.Headers.TryGetValue("User", out string userValue))
            {
                if (int.TryParse(userValue, out int userId))
                {
                    // Set the current user in the server environment
                    ServerEnvironment.SetCurrentUser(userId);
                }
                else
                {
                }
            }

            // Pass the message through unchanged
            return message;
        }
    }
}
