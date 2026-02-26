using Client;
using System;
using System.Threading.Tasks;
using ClientBehaviours = Client.ClientBehaviours;

// Add code here to send requests

Console.WriteLine("Enter endpoint (e.g. /ping), or blank to exit:");

while (true)
{
    Console.Write("Endpoint: ");
    string? endpoint = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(endpoint))
    {
        break;
    }

    Console.Write("Message: ");
    string? message = Console.ReadLine() ?? string.Empty;

    // This will connect to 127.0.0.1:5000 and send the request using your existing serializer
    ClientBehaviours.SendRequest(message, endpoint);
}

Console.WriteLine("Execution finished");
Console.ReadLine();