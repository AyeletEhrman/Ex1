using System.Net.Sockets;
namespace ServerProject
{
    interface IController
    {
        TaskResult ExecuteCommand(string commandLine, TcpClient client);
        string CommandType(string commandLine);
    }
}
