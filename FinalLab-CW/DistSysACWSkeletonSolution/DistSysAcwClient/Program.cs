using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;

#region Task 10 and beyond

string baseUrl = "http://localhost:53415";
string? storedUsername = null;
string? storedApiKey = null;
HttpClient client = new HttpClient();

Console.WriteLine("Hello. What would you like to do?");

while (true)
{
    string? input = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(input)) continue;

    Console.Clear();

    if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase))
        break;

    Console.WriteLine("...please wait...");

    string output = await HandleInput(input);
    Console.WriteLine(output);
    Console.WriteLine("\nWhat would you like to do next?");
}

async Task<string> HandleInput(string input)
{
    string[] parts = input.Split(' ', 3);
    string command = parts.Length >= 2 ? $"{parts[0]} {parts[1]}" : input;
    //calls methods need to build them next 
    return command.ToLower() switch
    {
        "talkback hello" => await TalkBackHello(),
        "talkback sort" => await TalkBackSort(parts),
        _ => "Unkown Command"
        //"user get" => await UserGet(parts),
        //"user post" => await UserPost(parts),
        //"user set" => UserSet(parts),
        //"user role" => await UserRole(parts),
        //"protected hello" => await ProtectedHello(),
        //"protected sha1" => await ProtectedSHA1(parts),
        //"protected sha256" => await ProtectedSHA256(parts),
        //"protected getpublickey" => await ProtectedGetPublicKey(),
        //"protected sign" => await ProtectedSig(parts),
        //"protected mashify" => await ProtectedMashify(parts),_=> "Unkown Command"
    };

}
//this is one of the methods seems like were calling to a specific part of the URL?
async Task<string> TalkBackHello()
{
    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/talkback/hello");
    return await response.Content.ReadAsStringAsync();
}
//implemented first two methods 
async Task<string> TalkBackSort(string[] parts)
{
    if (parts.Length < 3) return "No integers provided";

    string cleaned = parts[2].Trim('[', ']');
    string[] numbers = cleaned.Split(',');

    string query = string.Join("&", numbers.Select(n => $"integers={n.Trim()}"));
    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/talkback/sort?{query}");
    return await response.Content.ReadAsStringAsync();
}
#endregion