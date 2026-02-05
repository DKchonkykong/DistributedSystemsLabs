using PipesAndFilters.Messages;
using System;
using System.Globalization;
using System.Text;

namespace PipesAndFilters.Filters
{
    // translatefilter through IFilter interface checks headers in how to format the message request and how to convert from ASCII to bytes etc.
    internal class TranslateFilter : IFilter
    {
        //this now houses three more filters than just bytes now having binary and hex as well
        public IMessage Run(IMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            if (message.Headers != null && message.Headers.TryGetValue("RequestFormat", out var requestFormat))
            {
                if (!string.IsNullOrEmpty(message.Body))
                {
                    if (string.Equals(requestFormat, "Bytes", StringComparison.OrdinalIgnoreCase))
                    {
                        var byteStrings = message.Body.Split('-', StringSplitOptions.RemoveEmptyEntries);
                        var bytes = new byte[byteStrings.Length];
                        for (int i = 0; i < byteStrings.Length; i++)
                        {
                            bytes[i] = byte.Parse(byteStrings[i]);
                        }

                        message.Body = Encoding.ASCII.GetString(bytes);
                    }
                    else if (string.Equals(requestFormat, "Binary", StringComparison.OrdinalIgnoreCase))
                    {
                        var binaryStrings = message.Body.Split('-', StringSplitOptions.RemoveEmptyEntries);
                        var bytes = new byte[binaryStrings.Length];
                        for (int i = 0; i < binaryStrings.Length; i++)
                        {
                            bytes[i] = Convert.ToByte(binaryStrings[i], 2);
                        }

                        message.Body = Encoding.ASCII.GetString(bytes);
                    }
                    else if (string.Equals(requestFormat, "Hex", StringComparison.OrdinalIgnoreCase))
                    {
                        string hex = message.Body.Replace("-", string.Empty);
                        int len = hex.Length / 2;
                        var bytes = new byte[len];
                        for (int i = 0; i < len; i++)
                        {
                            bytes[i] = byte.Parse(hex.AsSpan(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                        }

                        message.Body = Encoding.ASCII.GetString(bytes);
                    }
                }
            }

            if (message.Headers != null && message.Headers.TryGetValue("ResponseFormat", out var responseFormat))
            {
                if (!string.IsNullOrEmpty(message.Body))
                {
                    var bytes = Encoding.ASCII.GetBytes(message.Body);

                    if (string.Equals(responseFormat, "Bytes", StringComparison.OrdinalIgnoreCase))
                    {
                        var bodyBuilder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bodyBuilder.Append(bytes[i].ToString());
                            if (i + 1 < bytes.Length)
                            {
                                bodyBuilder.Append("-");
                            }
                        }

                        message.Body = bodyBuilder.ToString();
                    }
                    else if (string.Equals(responseFormat, "Binary", StringComparison.OrdinalIgnoreCase))
                    {
                        var bodyBuilder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bodyBuilder.Append(Convert.ToString(bytes[i], 2).PadLeft(8, '0'));
                            if (i + 1 < bytes.Length)
                            {
                                bodyBuilder.Append("-");
                            }
                        }

                        message.Body = bodyBuilder.ToString();
                    }
                    else if (string.Equals(responseFormat, "Hex", StringComparison.OrdinalIgnoreCase))
                    {
                        var bodyBuilder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bodyBuilder.Append(bytes[i].ToString("X2", CultureInfo.InvariantCulture));
                            if (i + 1 < bytes.Length)
                            {
                                bodyBuilder.Append("-");
                            }
                        }

                        message.Body = bodyBuilder.ToString();
                    }
                }
            }

            return message;
        }
    }
}