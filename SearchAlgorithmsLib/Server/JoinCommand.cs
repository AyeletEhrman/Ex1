using MazeLib;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// class for the Join command.
    /// </summary>
    class JoinCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;

        /// <summary>
        /// constroctor of the join command.
        /// </summary>
        /// <param name="md"> the model</param>
        public JoinCommand(IModel<Maze> model)
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
            // needs to be only one argument.
            if (args.Length != 1)
            {
                return new TaskResult("bad args", false);
            }
            try
            {
                string name = args[0];
               // get the maze to join.
                Maze maze = model.Join(client, name);
                // maze not found.
                if (maze == null)
                {
                    return new TaskResult(null, false);
                }
                // return the json of the joined maze, and keep clients connected.
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
