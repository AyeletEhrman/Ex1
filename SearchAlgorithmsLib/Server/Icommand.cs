using System.Net.Sockets;

namespace ServerProject
{
    interface ICommand
    {
        TaskResult Execute(string[] args, TcpClient client = null);
    }
}
