using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerProject
{
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            
            new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            }).Start();
        }
        public void Stop()
        {
            listener.Stop();
        }
    }
}
