using MazeLib;
using System;
using System.Net.Sockets;
namespace ServerProject
{
    /// <summary>
    /// class for the StartGame command.
    /// </summary>
    class StartGameCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;

        /// <summary>
        /// constroctor of the StartGame command.
        /// </summary>
        /// <param name="md"> the model</param>
        public StartGameCommand(IModel<Maze> model)
        {
            this.model = model;
        }

        // <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">the arguments for the command.</param>
        /// <param name="client">the client that gave the command</param>
        /// <returns></returns>
        public TaskResult Execute(string[] args, TcpClient client = null)
        {
            // needs to be 3 arguments
            if (args.Length != 3)
            {
                return new TaskResult("bad args", false);
            }
            try
            {
                // name of the game.
                string name = args[0];
                // num of rows and colloms.
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                // start game.
                Maze maze = model.Start(client, name, rows, cols);
                // error.
                if (maze == null)
                {
                    return new TaskResult(null, false);
                }
                // return the maze in jason after someonr joined.
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
