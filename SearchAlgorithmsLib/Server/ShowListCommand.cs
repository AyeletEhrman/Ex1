using MazeLib;
using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// class for the ShowList command.
    /// </summary>
    class ShowListCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;

        /// <summary>
        /// constroctor of the ShowList command.
        /// </summary>
        /// <param name="md"> the model</param>
        public ShowListCommand(IModel<Maze> model)
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
            // return the list of the games you can join.
            return new TaskResult(model.List(), false);
        }
    }
}
