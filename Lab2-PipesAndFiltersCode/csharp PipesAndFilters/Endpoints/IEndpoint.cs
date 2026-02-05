using PipesAndFilters.Messages;

namespace PipesAndFilters
{
    public interface IEndpoint
    {
        IMessage Execute(IMessage message);
    }
}