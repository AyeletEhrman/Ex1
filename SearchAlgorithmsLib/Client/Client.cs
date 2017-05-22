using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ClientProject
{
    /// <summary>
    /// the client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// the end point connected.
        /// </summary>
        private IPEndPoint ep;
        /// <summary>
        /// the client.
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// flag for ending a multiplayers connection.
        /// </summary>
        private bool endMulti;
        /// <summary>
        /// the clients command.
        /// </summary>
        string commandLine;

        /// <summary>
        /// the clients constroctor.
        /// </summary>
        /// <param name="endPoint">the end point connected.</param>
        public Client(IPEndPoint endPoint)
        {
            ep = endPoint;
            endMulti = false;
            commandLine = null;
        }

        /// <summary>
        /// starts the connection with the clients.
        /// </summary>
        public string Send(string commandLine)
        {
            
            // open a tcpClient connection.
            client = new TcpClient();
            //  connect to server.
            client.Connect(ep);
                

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                try
                {
                    // write the command to server.
                    writer.Write(commandLine);
                    writer.Flush();
                    commandLine = null;
                    // gets the type of game.
                    string type = reader.ReadString();
                    Console.WriteLine(type);
                    // run single player game.
                    if (type.Equals("single"))
                    {
                        return Single(client, reader);
                    }
                    // run multiplayer game.
                    else if (type.Equals("multi"))
                    {
                        endMulti = false;
                        Multi(client, reader, writer);
                        while (!endMulti)
                        {
                            Thread.Sleep(100);
                        }
                        // finshed the game.
                        client.Close();
                    }
                    // error - close client.
                    else
                    {
                        client.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        /// <summary>
        /// run a single game.
        /// </summary>
        /// <param name="client">the client connected.</param>
        /// <param name="reader">the clients reader streams</param>
        private string Single(TcpClient client, BinaryReader reader)
        {
            // get result for the command.
            string result = reader.ReadString();
            // close connection.
            client.Close();
            return result;
        }

        /// <summary>
        /// run a multiplayer game.
        /// </summary>
        /// <param name="client">the client connected.</param>
        /// <param name="reader">the clients reader streams</param>
        /// <param name="writer">the clients writer stream</param>
        private void Multi(TcpClient client, BinaryReader reader, BinaryWriter writer)
        {
            bool disconnected = false;
            bool waiting = false;

            // writing task.
            Task writeTask = new Task(() =>
            {
                // while the client is still connected.
                while (!disconnected)
                {
                    if (!waiting)
                    {
                        try
                        {
                            if (commandLine == null)
                            {
                                commandLine = Console.ReadLine();
                            }
                            
                            if (disconnected)
                            {
                                break;
                            }
                            // write the next command to server.
                            writer.Write(commandLine);
                            writer.Flush();
                            commandLine = null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);

                        }
                    }
                }
                endMulti = true;
            });
            writeTask.Start();
            
            // reading task.
            Task readTask = new Task(() =>
            {
                // while the client is still connected.
                while (!disconnected)
                {
                    try
                    {
                        // geting the result for the command.
                        string result = reader.ReadString();
                        // game is over - end connection.
                        if (result.Equals("game over"))
                        {
                            writer.Write("game over");
                            writer.Flush();
                            // client disconnected.
                            disconnected = true;
                        }
                        // error or need to disconect.
                        else if (result.Equals("disconnect") || result.Equals("error occured"))
                        {
                            Console.WriteLine("disconnect");
                            // client disconnected.
                            disconnected = true;
                        }
                        // witing for join.
                        else if (result.Equals("wait"))
                        {
                            waiting = true;
                        }
                        else
                        {
                            Console.WriteLine(result);
                        }
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("catch error");
                        Thread.Sleep(10);
                    }
                }
            });
            readTask.Start();
        }
    }
}