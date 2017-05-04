using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// client handler interface (view) of the MVC.
    /// </summary>
    interface IClientHandler
    {
        void SetController(IController controller);
        void HandleClient(TcpClient client);
        void SendMessage(TcpClient client, string message);
    }
}
