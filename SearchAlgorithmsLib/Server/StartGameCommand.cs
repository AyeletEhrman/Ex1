﻿using MazeLib;
using System;
using System.Net.Sockets;
namespace ServerProject
{
    class StartGameCommand : ICommand
    {
        private IModel<Maze> model;
        public StartGameCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public TaskResult Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 3)
            {
                return new TaskResult("bad args", false);
            }
            try
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                Maze maze = model.Start(client, name, rows, cols);
                if (maze == null)
                {
                    return new TaskResult(null, false);
                }
                return new TaskResult(maze.ToJSON(), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
