using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject
{
    /// <summary>
    /// runs the client.
    /// </summary>
    class ClientMain
    {
        static void Main(string[] args)
        {
            // open end poin  connection.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1116);
            Client client = new Client(ep);
            // start client.
            client.Start();
            Console.ReadKey();
        }
    }
}
