using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;

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
            commands.Add("start", new StartGameCommand(model));
            commands.Add("list", new ShowListCommand(model));
            commands.Add("join", new JoinCommand(model));
            commands.Add("play", new PlayCommand(model, clientHandler));
            commands.Add("close", new CloseCommand(model, clientHandler));
        }

        public string CommandType(string commandLine)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (commandKey.Equals("generate") || commandKey.Equals("solve") || commandKey.Equals("list"))
            {
                return "single";
            }
            else if (commandKey.Equals("start") || commandKey.Equals("play") ||
                       commandKey.Equals("join") || commandKey.Equals("close"))
            {
                return "multi";
            }
            else
            {
                return "";
            }
        }
        public TaskResult ExecuteCommand(string commandLine, TcpClient client)
        {
            try
            {
                string[] arr = commandLine.Split(' ');
                string commandKey = arr[0];
                if (!commands.ContainsKey(commandKey))
                    return new TaskResult("Command not found", false);
                string[] args = arr.Skip(1).ToArray();
                ICommand command = commands[commandKey];
                return command.Execute(args, client);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
