using CommandLayer;
using Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrinityCoreWebAPI.Controllers
{
    [Route("api/[action]")]
    public class CommandsController : Controller
    {
        private IServerInfoCommand m_ServerInfoCommand;
        private ICreateAccountCommand m_CreateAccountCommand;

        public CommandsController(
            IServerInfoCommand serverInfoCommand,
            ICreateAccountCommand createAccountCommand)
        {
            m_ServerInfoCommand = serverInfoCommand;
            m_CreateAccountCommand = createAccountCommand;
        }
        
        [HttpPost]
        public async Task<IEnumerable<string>> ServerInfo()
        {
            return await m_ServerInfoCommand.Execute();
        }

        [HttpPost]
        public async Task<bool> CreateAccount([FromBody]CreateAccountRequest request)
        {
            return await m_CreateAccountCommand.Execute(request.Username, request.Password);
        }
    }
}
