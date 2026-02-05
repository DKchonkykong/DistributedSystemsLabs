using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;

namespace PipesAndFilters.Filters
{
    //timestapfilter inherits from ifilter making sure that the same message returns so rest of pipeline can continue using
    internal class TimestampFilter : IFilter
    {
        public IMessage Run(IMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            if (message.Headers == null)
            {
                message.Headers = new Dictionary<string, string>();
            }

            message.Headers["Timestamp"] = DateTime.Now.ToString();

            return message;
        }
    }
}