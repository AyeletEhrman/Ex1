using MazeLib;
using System;
using System.Net.Sockets;
namespace ServerProject
{
    class GenerateMazeCommand : ICommand
    {
        private IModel<Maze> model;
        public GenerateMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public TaskResult Execute(string[] args, TcpClient client)
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
                Maze maze = model.Generate(name, rows, cols);
                return new TaskResult(maze.ToJSON(), false);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
