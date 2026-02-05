using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Messages
{
    internal class Messages : IMessage
    {
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        //constructor initiates headers dictionary
        public Messages()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
        }
    }
}
