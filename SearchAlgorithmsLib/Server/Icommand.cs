using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// the command interface of the MVC.
    /// </summary>
    interface ICommand
    {
        TaskResult Execute(string[] args, TcpClient client = null);
    }
}
