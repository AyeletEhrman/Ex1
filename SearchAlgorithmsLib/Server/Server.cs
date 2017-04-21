using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// the class of the server.
    /// </summary>
    class Server
    {
        /// <summary>
        /// the port for the connection.
        /// </summary>
        private int port;
        /// <summary>
        /// the listener of the client.
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// 
        /// the client handler (view)
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// the constroctor of the Server.
        /// </summary>
        /// <param name="port">the port of the connection</param>
        /// <param name="ch">the client handler</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// start the running of the server.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            // start listening.
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            // open a task for the connection eith a clients.
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
