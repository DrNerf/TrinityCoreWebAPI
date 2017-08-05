using System;
using System.Collections.Generic;

namespace CommandLayer
{
    /// <summary>
    /// Commands factory.
    /// </summary>
    public class CommandFactory
    {
        private WorldServerConfiguration m_WorldServerConfiguration;
        private Dictionary<Type, object> m_Commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFactory"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CommandFactory(WorldServerConfiguration configuration)
        {
            m_WorldServerConfiguration = configuration;

            m_Commands = new Dictionary<Type, object>()
            {
                { typeof(IServerInfoCommand), new ServerInfoCommand(m_WorldServerConfiguration) },
                { typeof(ICreateAccountCommand), new CreateAccountCommand(m_WorldServerConfiguration) }
            };
        }

        public T Create<T>()
        {
            object command = null;
            if (m_Commands.TryGetValue(typeof(T), out command))
            {
                return (T)command;
            }
            else
            {
                throw new InvalidOperationException("No such command registered.");
            }
        }
    }
}
