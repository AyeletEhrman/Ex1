using System.Net.Sockets;
namespace ServerProject
{
    interface IClientHandler
    {
        void SetController(IController controller);
        void HandleClient(TcpClient client);
        void SendMessage(TcpClient client, string message);
    }
}
