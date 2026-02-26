using Client;
using System;
using System.Threading.Tasks;
using ClientBehaviours = Client.ClientBehaviours;

// Add code here to send requests now done async
Task t1 = ClientBehaviours.SendRequest("Hello World", "TaskOne");
Task t2 = ClientBehaviours.SendRequest("Hello World", "TaskTwo");
Task t3 = ClientBehaviours.SendRequest("Hello World", "TaskThree");

await Task.WhenAll(t1, t2, t3);

Console.WriteLine("Execution finished");
Console.ReadLine();