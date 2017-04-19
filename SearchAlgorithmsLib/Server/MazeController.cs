﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class MazeController : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel<Maze> model;
        private IClientHandler clientHandler;
        public MazeController(IModel<Maze> md, IClientHandler ch)
        {
            model = md;
            clientHandler = ch;
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartGameCommand());
            commands.Add("list", new ShowListCommand());
            commands.Add("join", new JoinCommand());
            commands.Add("play", new PlayCommand());
            commands.Add("close", new CloseCommand());
        }
     /*   public MazeController()
        {
            model = new MazeModel();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand());
            commands.Add("start", new StartGameCommand());
            commands.Add("list", new ShowListCommand());
            commands.Add("join", new JoinCommand());
            commands.Add("play", new PlayCommand());
            commands.Add("close", new CloseCommand());
        }*/

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            try
            {
                string[] arr = commandLine.Split(' ');
                string commandKey = arr[0];
                if (!commands.ContainsKey(commandKey))
                    return "Command not found";
                string[] args = arr.Skip(1).ToArray();
                ICommand command = commands[commandKey];
                return command.Execute(args, client);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "error occured";
            }
           
        }
    }
}
