using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ClientProject
{
    class Client
    {
        private IPEndPoint ep;
        private TcpClient client;
        private bool endMulti;
        string commandLine;

        public Client(IPEndPoint endPoint)
        {
            ep = endPoint;
            endMulti = false;
            commandLine = null;
        }

        public void Start()
        {
            while (true)
            {
                if (commandLine == null)
                {
                    commandLine = Console.ReadLine();
                }

                client = new TcpClient();
                client.Connect(ep);

                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    try
                    {
                        writer.Write(commandLine);
                        writer.Flush();
                        commandLine = null;
                        string type = reader.ReadString();
                        Console.WriteLine(type);
                        if (type.Equals("single"))
                        {
                            Single(client, reader);
                        }
                        else if (type.Equals("multi"))
                        {
                            endMulti = false;
                            Multi(client, reader, writer);
                            while (!endMulti)
                            {
                                Thread.Sleep(100);
                            }
                            client.Close();
                            continue;
                        }
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
        }

        private void Single(TcpClient client, BinaryReader reader)
        {
            string result = reader.ReadString();
            Console.WriteLine(result);
            client.Close();
        }
        
        private void Multi(TcpClient client, BinaryReader reader, BinaryWriter writer)
        {
            bool disconnected = false;
            bool waiting = false;

            Task writeTask = new Task(() =>
            {
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
            

            Task readTask = new Task(() =>
            {
                while (!disconnected)
                {
                    try
                    {
                        string result = reader.ReadString();
                        if (result.Equals("game over"))
                        {
                            writer.Write("game over");
                            writer.Flush();
                            // client disconnected.
                            disconnected = true;
                        }
                        else if (result.Equals("disconnect") || result.Equals("error occured"))
                        {
                            Console.WriteLine("disconnect");
                            // client disconnected.
                            disconnected = true;
                        }
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