using PipesAndFilters.Messages;
using System.Collections.Generic;

namespace PipesAndFilters
{
    public class RegisterUserEndpoint : IEndpoint
    {
        public IMessage Execute(IMessage message)
        {
            string username = message.Body;

            int newId = ServerEnvironment.RegisterUser(username);

            IMessage response = new PipesAndFilters.Messages.Messages();
            response.Headers = new Dictionary<string, string>();
            response.Headers.Add("ResponseFormat", message.Headers["RequestFormat"]);
            response.Body = $"Registered user '{username}' with ID {newId}";

            return response;
        }
    }
}