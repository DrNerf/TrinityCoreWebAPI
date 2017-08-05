using System.Threading.Tasks;

namespace CommandLayer
{
    /// <summary>
    /// Create Account Command.
    /// </summary>
    public interface ICreateAccountCommand : ICommand<bool>
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The command result.</returns>
        Task<bool> Execute(string username, string password);
    }
}
