using Client;
using System;
using System.Threading.Tasks;
using ClientBehaviours = Client.ClientBehaviours;

// Add code here to send requests now done async
Task t1 = ClientBehaviours.SendRequest("Hello World", "TaskOne");
Task t2 = ClientBehaviours.SendRequest("Hello World", "TaskTwo");
Task t3 = ClientBehaviours.SendRequest("Hello World", "TaskThree");
//one way of doing this the task waiting bit but this is kind of inefficent since you are doing each task individually instead of just doing it for all
//t1.Wait();
//t2.Wait();
//t3.Wait();
//another way of doing this but I think both of them are quite similar it just this ne uses the await keyword instead of just wait 
//nvm it actually executes them all in sequence T3 then T2 and then after 10 seconds T1
await Task.WhenAll(t1, t2, t3);

Console.WriteLine("Execution finished");
Console.ReadLine();