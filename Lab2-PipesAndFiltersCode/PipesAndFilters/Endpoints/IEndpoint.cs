using PipesAndFilters.Messages;

namespace PipesAndFilters
{
    // used by helloWord endpoint for processing messages
    public interface IEndpoint
    {
        IMessage Execute(IMessage message);
    }
}