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
    string[] parts = input.Split(' ', 4);
    string command = parts.Length >= 2 ? $"{parts[0]} {parts[1]}" : input;
    //calls methods need to build them next 
    return command.ToLower() switch
    {
        "talkback hello" => await TalkBackHello(),
        "talkback sort" => await TalkBackSort(parts),
        "user get" => await UserGet(parts),
        "user post" => await UserPost(parts),
        "user set" => UserSet(parts),
        "user delete" => await UserDelete(),
        "user role" => await UserRole(parts),
        "protected hello" => await ProtectedHello(),
        "protected sha1" => await ProtectedSHA1(parts),
        "protected sha256" => await ProtectedSHA256(parts),
        _ => "Unkown Command"
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

//this is the user methods
async Task<string> UserGet(string[] parts)
{
    if (parts.Length < 3) return "No username provided";
    string message = Uri.EscapeDataString(parts[2]);
    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/user/new?username={message}");
    return await response.Content.ReadAsStringAsync();
}

async Task<string> UserPost(string[] parts)
{
    if (parts.Length < 3) return "No username provided";
    string username = parts[2];
    var content = new StringContent($"\"{username}\"", System.Text.Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync($"{baseUrl}/api/user/new", content);
    if (response.IsSuccessStatusCode)
    {
        string apiKey = await response.Content.ReadAsStringAsync();
        storedUsername = username;
        storedApiKey = apiKey;
        storedApiKey = apiKey.Trim('"');
        return "Got API Key";
    }

    return await response.Content.ReadAsStringAsync

();
}

async Task<string> UserRole(string[] parts)
{
    if (storedApiKey == null)
        return "You need to do a User Post or User Set first";
    if (parts.Length < 4) return "Usage: User Role <username> <role>";
    SetApiKeyHeader();
    var body = new { username = parts[2], role = parts[3] };
    var content = new StringContent(
        System.Text.Json.JsonSerializer.Serialize(body),
        System.Text.Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync(
        $"{baseUrl}/api/user/changerole", content);
    return await response.Content.ReadAsStringAsync();
}
string UserSet(string[] parts)
{
    if (parts.Length < 4)
    {
        return "Usage: User Set <username> <apikey>";
    }

    storedUsername = parts[2];
    storedApiKey = parts[3];

    return "Stored";
}
async Task<string> UserDelete()
{
    if (storedApiKey == null || storedUsername == null)
    {
        return "You need to do a User Post or User Set first";
    }

    client.DefaultRequestHeaders.Remove("ApiKey");
    client.DefaultRequestHeaders.Add("ApiKey", storedApiKey);

    string username = Uri.EscapeDataString(storedUsername);

    HttpResponseMessage response = await client.DeleteAsync(
        $"{baseUrl}/api/user/removeuser?username={username}");

    return await response.Content.ReadAsStringAsync();
}

//gonna add the rest of the protected methods here

async Task<string> ProtectedHello()
{
    if (storedApiKey == null)
        return "You need to do a User Post or User Set first";
    SetApiKeyHeader();
    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/protected/hello");
    return await response.Content.ReadAsStringAsync();
}

async Task<string> ProtectedSHA1(string[] parts)
{
    if (storedApiKey == null)
        return "You need to do a User Post or User Set first";
    if (parts.Length < 3) return "No message provided";
    SetApiKeyHeader();
    string message = Uri.EscapeDataString(parts[2]);
    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/api/protected/sha1?message={message}");
    return await response.Content.ReadAsStringAsync();
}
void SetApiKeyHeader()
{
    client.DefaultRequestHeaders.Remove("ApiKey");
    client.DefaultRequestHeaders.Add("ApiKey", storedApiKey);
}
async Task<string> ProtectedSHA256(string[] parts)
{
    if (storedApiKey == null)
        return "You need to do a User Post or User Set first";
    if (parts.Length < 3) return "No message provided";
    client.DefaultRequestHeaders.Remove("ApiKey");
    client.DefaultRequestHeaders.Add("ApiKey", storedApiKey);
    string message = Uri.EscapeDataString(parts[2]);
    HttpResponseMessage response = await client.GetAsync(
        $"{baseUrl}/api/protected/sha256?message={message}");
    return await response.Content.ReadAsStringAsync();
}


#endregion