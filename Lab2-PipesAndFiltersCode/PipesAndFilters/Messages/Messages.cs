using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Messages
{
    public class Messages : IMessage
    {
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        //constructor initiates headers dictionary now actually initialized
        public Messages()
        {
            Headers = new Dictionary<string, string>();
        }
    }
}
