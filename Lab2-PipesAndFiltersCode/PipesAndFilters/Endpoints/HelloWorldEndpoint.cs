using PipesAndFilters.Messages;
using System.Collections.Generic;

namespace PipesAndFilters
{
    public class HelloWorldEndpoint : IEndpoint
    {
        public IMessage Execute(IMessage message)
        {
            IMessage response = new PipesAndFilters.Messages.Messages(); //it now works after forcing it to choose that class it kept complaining it was a namespace
            response.Headers = new Dictionary<string, string>();

            response.Body = $"Hello {ServerEnvironment.CurrentUser.Name}! You sent the message: {message.Body}";
            response.Headers.Add("ResponseFormat", message.Headers["RequestFormat"]);

            return response;
        }
    }
}
