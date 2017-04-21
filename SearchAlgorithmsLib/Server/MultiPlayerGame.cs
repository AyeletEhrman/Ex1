using System.Net.Sockets;
using System.Threading;

namespace ServerProject
{
    class MultiPlayerGame
    {
        private TcpClient client1;
        private TcpClient client2;
        private bool twoPlayers;
        public string Name { get; }

        public MultiPlayerGame(TcpClient client, string name)
        {
            client1 = client;
            twoPlayers = false;
            Name = name;
        }
        public void WaitForJoin()
        {
            while (!twoPlayers)
            {   
                Thread.Sleep(100);
            }
        }
        public void SetSecondPlayer(TcpClient client)
        {
            client2 = client;
            twoPlayers = true;
        }
        public bool IsPlayer(TcpClient player)
        {
            if (player == client1 || player == client2)
            {
                return true;
            }
            return false;
        }

        public TcpClient GetOpponent(TcpClient player1)
        {
            if (player1 == client1)
            {
                return client2;
            } else if (player1 == client2)
            {
                return client1;
            } else
            {
                return null;
            }
        }
    }
}
