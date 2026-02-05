using PipesAndFilters.Filters;
using PipesAndFilters.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Pipes
{
    internal interface IPipe
    {
        public IMessage ProcessMessage(IMessage message);
        public IFilter RegisterFilter(IFilter filter);
    }
}
