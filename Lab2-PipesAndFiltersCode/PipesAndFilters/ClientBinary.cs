using System;
using System.Text;
using PipesAndFilters.Messages;

namespace PipesAndFilters
{
    //for managing binary compared to regular client which handles bytes
    class ClientBinary
    {
        int userId = 1;

        public void RequestHello(string messageToSend)
        {
            IMessage message = new PipesAndFilters.Messages.Messages();

            message.Headers.Add("User", userId.ToString());
            message.Headers.Add("RequestFormat", "Binary");
            message.Headers.Add("Endpoint", "Hello");

            byte[] bytes = Encoding.ASCII.GetBytes(messageToSend);
            string requestBody = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                requestBody += Convert.ToString(bytes[i], 2).PadLeft(8, '0');
                if (i + 1 < bytes.Length)
                {
                    requestBody += "-";
                }
            }
            message.Body = requestBody;

            IMessage response = ServerEnvironment.SendRequest(message);

            response.Headers.TryGetValue("Timestamp", out string timestamp);

            string responseBody = "";
            string[] binaryStrings = response.Body.Split('-');
            bytes = new byte[binaryStrings.Length];
            for (int i = 0; i < binaryStrings.Length; i++)
            {
                bytes[i] = Convert.ToByte(binaryStrings[i], 2);
            }
            responseBody = Encoding.ASCII.GetString(bytes);

            Console.WriteLine($"At {timestamp} Response was: {responseBody}");
        }
    }
}