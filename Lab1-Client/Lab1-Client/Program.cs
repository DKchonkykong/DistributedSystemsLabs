using System.Net.Sockets;
using System.Text;


TcpClient tcpClient = new TcpClient();
tcpClient.Connect("127.0.0.1", 5000);
using NetworkStream nStream = tcpClient.GetStream();
Console.WriteLine("Enter a message to be translated...");
string? message = Console.ReadLine();
if (!string.IsNullOrEmpty(message))
{
    byte[] request = Serialize(message);
    nStream.Write(request, 0, request.Length);

    // should read response now? and print the display?
    int respLen = nStream.ReadByte();
    if (respLen < 0)
    {
        Console.WriteLine("No response received.");
    }
    else
    {
        byte[] resp = new byte[respLen];
        int read = 0;
        while (read < respLen)
        {
            int n = nStream.Read(resp, read, respLen - read);
            if (n == 0) break;
            read += n;
        }
        string translatedmessage = Encoding.ASCII.GetString(resp, 0, read); //this might be an issue?
        Console.WriteLine("Translated: " + translatedmessage);
    }
}

Console.ReadKey(); // Wait for keypress before exit

byte[] Serialize(string request)
{
    byte[] responseBytes = Encoding.ASCII.GetBytes(request);
    byte responseLength = (byte)responseBytes.Length;
    byte[] rawData = new byte[responseLength + 1];
    rawData[0] = responseLength;
    responseBytes.CopyTo(rawData, 1);
    return rawData;
}