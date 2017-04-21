using System.Net.Sockets;

namespace ServerProject
{
    interface IModel<T>
    {
        void SetController(IController cont);
        T Generate(string name, int x, int y);
        string Solve(string name, int searcher);
        T Start(TcpClient client, string name, int x, int y);
        T Join(TcpClient client, string name);
        string List();
        MultiPlayerGame GetGame(TcpClient player);
    }
}
