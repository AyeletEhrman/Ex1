using System.Net.Sockets;
namespace ServerProject
{
    /// <summary>
    /// the controller interface of the MVC.
    /// </summary>
    interface IController
    {
        /// <summary>
        /// execute the command.
        /// </summary>
        /// <param name="commandLine">the command from the client</param>
        /// <param name="client">the client who gave the command.</param>
        /// <returns>the result of the command</returns>
        TaskResult ExecuteCommand(string commandLine, TcpClient client);

        /// <summary>
        /// check if command type is single or multi
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns>the command type</returns>
        string CommandType(string commandLine);
    }
}
