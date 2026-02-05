using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Messages
{
    internal interface IMessage
    {
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

    }
}
