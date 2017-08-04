using System.Collections.Generic;

namespace CommandLayer
{
    public interface IServerInfoCommand : ICommand<IEnumerable<string>>
    {
    }
}
