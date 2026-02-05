using PipesAndFilters.Filters;
using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Pipes
{
    internal class Pipe : IPipe
    {
        //constructor
        private Pipe()
        {
            Filters = new List<IFilter>();
            Messages = new List<IMessage>();
        }
        //private list for filters and messages
        private List<IFilter> Filters { get; }
        private List<IMessage> Messages { get; }
        //this actually is the method that processes and filters the message
        public IMessage ProcessMessage(IMessage message)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));

            IMessage current = message;
            Messages.Add(current);

            foreach (var filter in Filters)
            {
                if (filter is null) continue;

                current = filter.Run(current) ?? throw new InvalidOperationException($"Filter {filter.GetType().Name} returned null.");
                Messages.Add(current);
            }

            return current;
        }
        //method to registers filters
        public IFilter RegisterFilter(IFilter filter)
        {
            if (filter is null) throw new ArgumentNullException(nameof(filter));

            Filters.Add(filter);
            return filter;
        }
    }
}