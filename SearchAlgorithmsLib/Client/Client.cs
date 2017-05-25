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
        public string CommandLine { get; set; }
        public bool NextCommand { get; set; }
        public string Answer { get; set; }
        public bool CommandReady { get; set; }


        /// <summary>
        /// the clients constroctor.
        /// </summary>
        /// <param name="endPoint">the end point connected.</param>
        public Client(IPEndPoint endPoint)
        {
            ep = endPoint;
            endMulti = false;
            CommandLine = null;
            NextCommand = false;
            Answer = null;
            CommandReady = false;
        }

        /// <summary>
        /// starts the connection with the clients.
        /// </summary>
        public void Send(string commandLine)
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
                        Answer = Single(client, reader);
                        return;
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
                            while (!CommandReady)// && !endMulti)
                            {
                                Thread.Sleep(100);
                                
                             /*   if (disconnected)
                                {
                                    break;
                                }*/
                                //CommandLine = Console.ReadLine();
                            }
                            CommandReady = false;

                            if (disconnected)
                            {
                                break;
                            }
                            // write the next command to server.
                            writer.Write(CommandLine);
                            writer.Flush();
                            CommandLine = null;
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
                            Answer = "game over";

                            // client disconnected.
                            disconnected = true;
                            NextCommand = true;
                            Console.WriteLine("updated gameover");//
                            endMulti = true;
                        }
                        // error or need to disconect.
                        else if (result.Equals("disconnect") || result.Equals("error occured"))
                        {
                            Console.WriteLine(result);
                            // client disconnected.

                            Answer = "game over";
                            disconnected = true;
                            NextCommand = true;
                            endMulti = true;
                            
                        }
                        // witing for join.
                        else if (result.Equals("wait"))
                        {
                            waiting = true;
                        }
                        else
                        {
                            Answer = result;
                            NextCommand = true;
                        }
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("catch error");
                        Thread.Sleep(10);
                    }
                }
                endMulti = true;
            });
            readTask.Start();
        }
    }
}