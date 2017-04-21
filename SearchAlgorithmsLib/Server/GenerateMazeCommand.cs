using MazeLib;
using System;
using System.Net.Sockets;
namespace ServerProject
{
    /// <summary>
    /// class for the GenerateMaze command.
    /// </summary>
    class GenerateMazeCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;

        /// <summary>
        /// constroctor of the generate maze command.
        /// </summary>
        /// <param name="md"> the model</param>
        public GenerateMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">the arguments for the command.</param>
        /// <param name="client">the client that gave the command</param>
        /// <returns></returns>
        public TaskResult Execute(string[] args, TcpClient client)
        {
            // needs to be 3 arguments.
            if (args.Length != 3)
            {
                return new TaskResult("bad args", false);
            }
            try
            {
                // name of maze to generate.
                string name = args[0];
                // num of rows and colloms
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                // generate maze.
                Maze maze = model.Generate(name, rows, cols);
                // return the json of the maze and tell the client to close.
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
