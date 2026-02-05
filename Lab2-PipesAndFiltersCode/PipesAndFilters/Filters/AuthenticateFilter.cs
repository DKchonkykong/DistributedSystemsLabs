using PipesAndFilters.Messages;
using System;

namespace PipesAndFilters.Filters
{
    internal class AuthenticateFilter : IFilter
    {
        //run method checks for user header and if valid sets current user in server enviroment then message passes unchanged should be working now
        public IMessage Run(IMessage message)
        {


            if (message == null) throw new ArgumentNullException(nameof(message));

            if (message.Headers != null && message.Headers.TryGetValue("User", out var userValue))
            {
                if (int.TryParse(userValue, out var userId))
                {
                    // Set the current user in the server environment
                    ServerEnvironment.SetCurrentUser(userId);

                }

            }
            // Pass the message through unchanged
            return message;
        }
    }
}
