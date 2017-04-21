using MazeLib;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// class for the SolveMaze command.
    /// </summary>
    class SolveMazeCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;

        /// <summary>
        /// constroctor of the SolveMaze command.
        /// </summary>
        /// <param name="md"> the model</param>
        public SolveMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }

        // <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">the arguments for the command.</param>
        /// <param name="client">the client that gave the command</param>
        /// <returns></returns>
        public TaskResult Execute(string[] args, TcpClient client)
        {
            // needes to be two arguments.
            if (args.Length != 2)
            {
                return new TaskResult("bad args", false);
            }
            try { 
                // name of the game to be solved.
                string name = args[0];
                // the algo for solving the maze.
                int algo = int.Parse(args[1]);
                // get the solution of the game.
                string solution = model.Solve(name, algo);
                // return the solution and tell the client to close.
                return new TaskResult(solution, false);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
