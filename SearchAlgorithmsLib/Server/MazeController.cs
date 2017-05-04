using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using MazeLib;

namespace ServerProject
{
    /// <summary>
    /// the maze controller, the controller of the maze MVC.
    /// </summary>
    class MazeController : IController
    {
        /// <summary>
        /// Dictonary of the commands.
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// model of the maze MVC.
        /// </summary>
        private IModel<Maze> model;
        /// <summary>
        /// clientHandler (view) of the maze MVC.
        /// </summary>
        private IClientHandler clientHandler;

        /// <summary>
        /// the constroctor of the maze controller
        /// </summary>
        /// <param name="md">the model</param>
        /// <param name="ch">the client handler</param>
        public MazeController(IModel<Maze> md, IClientHandler ch)
        {
            model = md;
            clientHandler = ch;
            // add commands to dictonary.
            commands = new Dictionary<string, ICommand>
            {
                { "generate", new GenerateMazeCommand(model) },
                { "solve", new SolveMazeCommand(model) },
                { "start", new StartGameCommand(model) },
                { "list", new ShowListCommand(model) },
                { "join", new JoinCommand(model) },
                { "play", new PlayCommand(model, clientHandler) },
                { "close", new CloseCommand(model, clientHandler) }
            };
        }

        /// <summary>
        /// check if command type is single or multi
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns>the command type</returns>
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
            // error.
            else
            {
                return "";
            }
        }

        /// <summary>
        /// execute the command.
        /// </summary>
        /// <param name="commandLine">the command from the client</param>
        /// <param name="client">the client who gave the command.</param>
        /// <returns>the result of the command</returns>
        public TaskResult ExecuteCommand(string commandLine, TcpClient client)
        {
            try
            {
                string[] arr = commandLine.Split(' ');
                string commandKey = arr[0];
                // command not matching.
                if (!commands.ContainsKey(commandKey))
                    return new TaskResult("Command not found", false);
                string[] args = arr.Skip(1).ToArray();
                ICommand command = commands[commandKey];
                // execute the command.
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
