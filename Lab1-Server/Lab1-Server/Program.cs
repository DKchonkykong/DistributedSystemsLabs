using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
tcpListener.Start();

using TcpClient tcpClient = tcpListener.AcceptTcpClient();
using NetworkStream nStream = tcpClient.GetStream();

string message = ReadFromStream(nStream);
Console.WriteLine("Received: \"" + message + "\"");

string translatedMessage = Translate(message);

// serialized the message so it uses the same encoding as the client it should work now
byte[] outBytes = Encoding.ASCII.GetBytes(translatedMessage);
if (outBytes.Length > 255)
{
    throw new InvalidOperationException("Translated message too long for single-byte-length protocol.");
}
byte[] rawData = new byte[outBytes.Length + 1];
rawData[0] = (byte)outBytes.Length;
Array.Copy(outBytes, 0, rawData, 1, outBytes.Length);
nStream.Write(rawData, 0, rawData.Length);
nStream.Flush();

Console.WriteLine("Sent: \"" + translatedMessage + "\"");
Console.ReadKey(); // Wait for keypress before exit

static string Translate(string message)
{
    if (string.IsNullOrEmpty(message)) return message;

    string[] words = message.Split(' ');
    for (int i = 0; i < words.Length; i++)
    {
        words[i] = TranslateToken(words[i]);
    }
    return string.Join(' ', words);
}

static string TranslateToken(string token)
{
    // Preserve surrounding non-letters i.e., punctuation
    int start = 0;
    while (start < token.Length && !char.IsLetter(token[start])) start++;
    int end = token.Length - 1;
    while (end >= 0 && !char.IsLetter(token[end])) end--;

    if (start > end) return token; // no letters

    string leading = token.Substring(0, start);
    string trailing = token.Substring(end + 1);
    string core = token.Substring(start, end - start + 1);

    string translatedCore = PigLatin(core);

    // Preserve capitalization style
    if (IsAllUpper(core))
    {
        translatedCore = translatedCore.ToUpperInvariant();
    }
    else if (IsTitleCase(core))
    {
        translatedCore = ToTitleCase(translatedCore);
    }

    return leading + translatedCore + trailing;
}

static string PigLatin(string word)
{
    if (string.IsNullOrEmpty(word)) return word;

    string vowels = "aeiouAEIOU";
    int firstVowel = -1;
    for (int i = 0; i < word.Length; i++)
    {
        if (vowels.IndexOf(word[i]) >= 0)
        {
            firstVowel = i;
            break;
        }
    }

    if (firstVowel == 0)
    {
        // starts with a vowel -> add "way"
        return word + "way";
    }
    else if (firstVowel > 0)
    {
        // move leading consonant cluster to the end and add "ay"
        return word.Substring(firstVowel) + word.Substring(0, firstVowel) + "ay";
    }
    else
    {
        // no vowel found -> add "ay"
        return word + "ay";
    }
}

// helper functions
static bool IsAllUpper(string s)
{
    foreach (char c in s)
    {
        if (char.IsLetter(c) && !char.IsUpper(c)) return false;
    }
    return true;
}

static bool IsTitleCase(string s)
{
    if (string.IsNullOrEmpty(s)) return false;
    return char.IsUpper(s[0]) && (s.Length == 1 || s.Substring(1) == s.Substring(1).ToLowerInvariant());
}

static string ToTitleCase(string s)
{
    if (string.IsNullOrEmpty(s)) return s;
    return char.ToUpperInvariant(s[0]) + (s.Length > 1 ? s.Substring(1).ToLowerInvariant() : string.Empty);
}

static string ReadFromStream(NetworkStream stream)
{
    int messageLength = stream.ReadByte();
    if (messageLength < 0) return string.Empty;

    byte[] messageBytes = new byte[messageLength];
    int read = 0;
    while (read < messageLength)
    {
        int n = stream.Read(messageBytes, read, messageLength - read);
        if (n == 0) break;
        read += n;
    }
    return Encoding.ASCII.GetString(messageBytes, 0, read);
}