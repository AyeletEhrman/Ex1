using MazeLib;
using System;
using System.Net.Sockets;

    namespace ServerProject
    { 
    /// <summary>
    /// class for the closing command.
    /// </summary>
    class CloseCommand : ICommand
    {
        /// <summary>
        /// model of the mvc.
        /// </summary>
        private IModel<Maze> model;
        /// <summary>
        /// client handler(view) of the mvc.
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// constroctor of the close command.
        /// </summary>
        /// <param name="md"> the model</param>
        /// <param name="ich">the client handler</param>
        public CloseCommand(IModel<Maze> md, IClientHandler ich)
        {
            model = md;
            ch = ich;
        }

        /// <summary>
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
                // find the game to close.
                MultiPlayerGame game = model.GetGame(client);
                // get the client we need to send him the close msg.
                TcpClient opp = game.GetOpponent(client);
                // error - eather no opp or no game.
                if (game == null || opp == null)
                {
                    return new TaskResult(null, false);
                }
                // send the opp the game is over.
                ch.SendMessage(opp, "game over");
                // disconnect the current client.
                return new TaskResult("disconnect", false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
