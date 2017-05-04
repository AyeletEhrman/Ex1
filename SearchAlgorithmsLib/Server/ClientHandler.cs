using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerProject
{
    /// <summary>
    /// clienHandler class, handels the commands from the client.
    /// </summary>
    class ClientHandler : IClientHandler
    {
        /// <summary>
        /// the controller of the MVC.
        /// </summary>
        private IController cont;

        /// <summary>
        /// Cliendhandler constructor.
        /// </summary>
        public ClientHandler()
        {
        }

        /// <summary>
        /// sets the controller of the MVC.
        /// </summary>
        /// <param name="controller">the controller to set</param>
        public void SetController(IController controller)
        {
            cont = controller;
        }

        /// <summary>
        /// the function to call for handeling the client.
        /// </summary>
        /// <param name="client">the client to handle.</param>
        public void HandleClient(TcpClient client)
        {
            // start a task fot handeling the client.
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
                            // read the connand line.
                            string commandLine = reader.ReadString();
                            // needs to close, because the game is over.
                            if (commandLine.Equals("game over"))
                            {
                                break;
                            }
                            // check if multi or single game.
                            string type = cont.CommandType(commandLine);
                            writer.Write(type);

                            Console.WriteLine("Got command: {0}", commandLine);
                            // execute the command.
                            TaskResult result = cont.ExecuteCommand(commandLine, client);

                            // we got in error while executing the command.
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
                            // client needs to disconnect.
                            if (result.JsonSol.Equals("disconnect"))
                            {
                                break;
                            }
                            // needs to close if single or other error type of game.
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

        /// <summary>
        /// send message to a ifferent client from the current one.
        /// </summary>
        /// <param name="client">the client that will get the message.</param>
        /// <param name="message">the message to send</param>
        public void SendMessage(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            try
            {
                writer.Write(message);
                writer.Flush();
            }
            // error sending.
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
