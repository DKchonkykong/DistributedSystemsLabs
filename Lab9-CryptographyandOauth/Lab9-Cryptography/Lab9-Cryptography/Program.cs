using System.Security.Cryptography;

//this seems to be just converting plaintext string into being more encrypted with bytes sha1 encoding and converting to hex

//we usin sha256 instead of sha1 it is a lot more secure since it has a longer hash 

//need to decrypt it now apparently two ways to do this Asymetric and symetric decryption

//asymetric can be done with RSA and is used in the TLS handshake for exchanging keys

//Can decrypt messages now wow!!

string message = "passageofwords";
Console.Write("Plaintext message: ");
Console.WriteLine(message);
byte[] encryptedBytes;
byte[] decryptedBytes;
byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(message); //converted the string into bytes via ascii
static string ByteArrayToHexString(byte[] byteArray)
{
    string hexString = "";
    if (null != byteArray)
    { foreach (byte b in byteArray)
        {
            hexString += b.ToString("X2"); //converting the byte array into a hexadecimal string
        }
    }
    return hexString;
}


//new version exported the key instead and now works huh 
using (RSA rsaEncrypt = RSA.Create())
using (RSA rsaDecrypt = RSA.Create())
{
    rsaDecrypt.ImportParameters(rsaEncrypt.ExportParameters(true));
    byte[] encrypted = rsaEncrypt.Encrypt(asciiByteMessage, RSAEncryptionPadding.OaepSHA1);
    byte[] decrypted = rsaDecrypt.Decrypt(encrypted, RSAEncryptionPadding.OaepSHA1);
    Console.Write("Encrypted message: ");
    Console.WriteLine(ByteArrayToHexString(encrypted));
    Console.Write("Decrypted message: ");
    Console.WriteLine(System.Text.Encoding.ASCII.GetString(decrypted));
}








//OLD bad version of encrypt and decrypt changing each time
//using (RSA rsaProvider = RSA.Create())
//{
//    // Encrypt
//    encryptedBytes = rsaProvider.Encrypt(asciiByteMessage, RSAEncryptionPadding.OaepSHA1);
//    Console.Write("Encrypted message: ");
//    Console.WriteLine(ByteArrayToHexString(encryptedBytes));
//    // Decrypt
//    decryptedBytes = rsaProvider.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA1);
//    Console.Write("Decrypted message: ");
//    Console.WriteLine(System.Text.Encoding.ASCII.GetString(decryptedBytes));
//}


















//OLD Encryption only version

//string plaintext = "Hello, World!";


//byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(plaintext); //converted the string into bytes via ascii

//byte[] sha1ByteMessage;
//using (SHA256 sha1Provider = SHA256.Create())
//{
//sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage); //hashing the byte message using sha1 encoding
//}


//static string ByteArrayToHexString(byte[] byteArray)
//{
//    string hexString = "";
//    if (null != byteArray)
//    { foreach (byte b in byteArray)
//        {
//            hexString += b.ToString("X2"); //converting the byte array into a hexadecimal string
//        }
//    }
//    return hexString;
//}


//Console.WriteLine(ByteArrayToHexString(sha1ByteMessage)); //printing the hex string to the console