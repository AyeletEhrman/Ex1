using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class ClientHandler : IClientHandler
    {
        private IController cont;
        public ClientHandler()//IController icont)
        {
            //cont = icont;
        }
        public void SetController(IController controller)
        {
            cont = controller;
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        Console.WriteLine("insert command on client");
                        try
                        {
                            string commandLine = reader.ReadString();
                            Console.WriteLine("Got command: {0}", commandLine);



                            string result = cont.ExecuteCommand(commandLine, client);
                            writer.Write(result);
                            writer.Flush();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                    }
                }
                // closes the cient socket.
                client.Close();
                // then what happens
            }).Start();
        }
    }
}
