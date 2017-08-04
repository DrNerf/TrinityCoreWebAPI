using CommandLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrinityCoreWebAPI.Controllers
{
    [Route("api/[action]")]
    public class CommandsController : Controller
    {
        private IServerInfoCommand m_ServerInfoCommand;

        public CommandsController(IServerInfoCommand serverInfoCommand)
        {
            m_ServerInfoCommand = serverInfoCommand;
        }
        
        [HttpPost]
        public async Task<IEnumerable<string>> ServerInfo()
        {
            return await m_ServerInfoCommand.Execute();
        }
    }
}
