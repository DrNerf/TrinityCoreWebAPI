using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommandLayer
{
    /// <summary>
    /// Command base class.
    /// </summary>
    internal abstract class CommandBase<T> : ICommand<T>
    {
        private const string m_CommandRequestTemplate = 
            @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"" xmlns:ns1=""urn:TC"">
                <SOAP-ENV:Header>
                </SOAP-ENV:Header>
                <SOAP-ENV:Body>
                        <ns1:executeCommand>
                            <command>{0}</command>
                        </ns1:executeCommand>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        private WorldServerConfiguration m_WorldServerConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CommandBase(WorldServerConfiguration configuration)
        {
            m_WorldServerConfiguration = configuration;
        }

        /// <summary>
        /// Calls the server command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The command result.</returns>
        protected async Task<string> CallServerCommand(string command)
        {
            try
            {
                using (var client = CreateClient())
                {
                    var requestContent = new StringContent(string.Format(m_CommandRequestTemplate, command));
                    var response = await client.PostAsync(
                        $"http://{m_WorldServerConfiguration.Address}:{m_WorldServerConfiguration.Port}",
                        requestContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var doc = XDocument.Parse(result);

                    var temp = doc.Descendants(XName.Get("result")).First().Value;
                    return doc.Descendants(XName.Get("result")).First().Value.Trim();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <returns>Creates a HttpClient with Authorization token.</returns>
        protected HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", GetEncodedCredentials());

            return client;
        }

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The string encoded in Base64.</returns>
        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Gets the encoded credentials.
        /// </summary>
        /// <returns>The credentials encoded in Base64.</returns>
        protected string GetEncodedCredentials()
        {
            return $"Basic {Base64Encode($"{m_WorldServerConfiguration.AccountName}:{m_WorldServerConfiguration.Password}")}";
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The command result.</returns>
        public abstract Task<T> Execute();
    }
}