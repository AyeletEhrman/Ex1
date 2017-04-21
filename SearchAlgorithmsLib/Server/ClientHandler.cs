using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerProject
{
    class ClientHandler : IClientHandler
    {
        private IController cont;
        public ClientHandler()
        {
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
                        try
                        {
                            string commandLine = reader.ReadString();
                            if (commandLine.Equals("game over"))
                            {
                                break;
                            }
                            string type = cont.CommandType(commandLine);
                            writer.Write(type);

                            Console.WriteLine("Got command: {0}", commandLine);

                            TaskResult result = cont.ExecuteCommand(commandLine, client);
                            if (result.JsonSol == null)
                            {
                                writer.Write("error occured");
                                writer.Flush();
                                break;
                            }
                            else if (!result.JsonSol.Equals(""))
                            {
                                writer.Write(result.JsonSol);
                                writer.Flush();
                            }
                            if (result.JsonSol.Equals("disconnect"))
                            {
                                break;
                            }
                                if (!type.Equals("multi"))
                            {
                                break;
                            }
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
            }).Start();
        }

        public void SendMessage(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            try
            {
                writer.Write(message);
                writer.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
