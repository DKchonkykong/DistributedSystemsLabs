using System;
using System.Globalization;
using System.Text;
using PipesAndFilters.Messages;

namespace PipesAndFilters
{
    //same as regular client except this handles hex instead of bytes and will convert hex message to ascii etc.
    class ClientHex
    {
        int userId = 1;

        public void RequestHello(string messageToSend)
        {
            IMessage message = new PipesAndFilters.Messages.Messages();

            message.Headers.Add("User", userId.ToString());
            message.Headers.Add("RequestFormat", "Hex");
            message.Headers.Add("Endpoint", "Hello");

            byte[] bytes = Encoding.ASCII.GetBytes(messageToSend);
            string requestBody = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                requestBody += bytes[i].ToString("X2", CultureInfo.InvariantCulture);
                if (i + 1 < bytes.Length)
                {
                    requestBody += "-";
                }
            }
            message.Body = requestBody;

            IMessage response = ServerEnvironment.SendRequest(message);

            response.Headers.TryGetValue("Timestamp", out string timestamp);

            string responseBody = "";
            string[] hexStrings = response.Body.Split('-');
            bytes = new byte[hexStrings.Length];
            for (int i = 0; i < hexStrings.Length; i++)
            {
                bytes[i] = byte.Parse(hexStrings[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            responseBody = Encoding.ASCII.GetString(bytes);

            Console.WriteLine($"At {timestamp} Response was: {responseBody}");
        }
    }
}