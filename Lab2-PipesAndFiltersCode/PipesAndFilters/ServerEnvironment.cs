using PipesAndFilters.Filters;
using PipesAndFilters.Messages;
using PipesAndFilters.Pipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PipesAndFilters
{
    static class ServerEnvironment
    {
        private static List<User> Users { get; set; }
        public static User CurrentUser { get; private set; }
        private static IPipe IncomingPipe { get; set; }
        private static IPipe OutgoingPipe { get; set; }
        private static Dictionary<string, IEndpoint> Endpoints { get; set; }

        public static void Setup()
        {
            Users = new List<User>();
            Users.Add(new User() { ID = 1, Name = "Test User" });

            IncomingPipe = new Pipe();
            OutgoingPipe = new Pipe();

            IncomingPipe.RegisterFilter(new AuthenticateFilter());
            IncomingPipe.RegisterFilter(new TranslateFilter());

            OutgoingPipe.RegisterFilter(new TranslateFilter());
            OutgoingPipe.RegisterFilter(new TimestampFilter());

            Endpoints = new Dictionary<string, IEndpoint>(StringComparer.OrdinalIgnoreCase);
            Endpoints["Hello"] = new HelloWorldEndpoint();
            Endpoints["Register"] = new RegisterUserEndpoint();
        }

        public static bool SetCurrentUser(int id)
        {
            foreach (var user in Users)
            {
                if (user.ID == id)
                {
                    CurrentUser = user;
                    return true;
                }
            }

            return false;
        }
        //helper method to register user and returning new user id
        public static int RegisterUser(string username)
        {
            int newId = Users.Count == 0 ? 1 : Users[^1].ID + 1;
            var user = new User { ID = newId, Name = username };
            Users.Add(user);
            return newId;
        }

        public static IMessage SendRequest(IMessage message)
        {
            // 1. Send the message through the incoming pipeline
            message = IncomingPipe.ProcessMessage(message);
            //this is for endpoint checking if not found throws exception
            if (message.Headers == null || !message.Headers.TryGetValue("Endpoint", out var endpointKey))
            {
                throw new InvalidOperationException("Endpoint header is missing.");
            }

            // 2. Send the message to the endpoint
            //this is now done through if statement checking if endpoint is found if not throws exception
            if (!Endpoints.TryGetValue(endpointKey, out var endpoint))
            {
                throw new InvalidOperationException($"Unknown endpoint '{endpointKey}'.");
            }
            message = endpoint.Execute(message);

            // 3. Send the message through the outgoing pipeline
            message = OutgoingPipe.ProcessMessage(message);

            // 4. Send the message back to the client
            return message;

        }

    }
}
