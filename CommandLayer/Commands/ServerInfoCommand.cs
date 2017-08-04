using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace CommandLayer
{
    internal class ServerInfoCommand : CommandBase<IEnumerable<string>>, IServerInfoCommand
    {
        public ServerInfoCommand(WorldServerConfiguration configuration) 
            : base(configuration)
        {
        }

        public override Task<IEnumerable<string>> Execute()
        {
            var commandTask = CallServerCommand("server info");
            return commandTask.ContinueWith(t =>
            {
                var commandResultRows = t.Result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                return commandResultRows as IEnumerable<string>;
            });
        }
    }
}
