using System.Threading.Tasks;

namespace CommandLayer
{
    /// <summary>
    /// Command.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The command result.</returns>
        Task<T> Execute();
    }
}
