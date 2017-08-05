using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandLayer
{
    internal class CreateAccountCommand : CommandBase<bool>, ICreateAccountCommand
    {
        public CreateAccountCommand(WorldServerConfiguration configuration) 
            : base(configuration)
        {
        }

        public override Task<bool> Execute()
        {
            throw new NotSupportedException();
        }

        public async Task<bool> Execute(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var commandResult = await CallServerCommand($"account create {username} {password}");
                return commandResult == $"Account created: {username}";
            }

            return false;
        }
    }
}
