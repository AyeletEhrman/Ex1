using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    /// <summary>
    /// class for the playing command.
    /// </summary
    class PlayCommand : ICommand
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
        /// constroctor of the play command.
        /// </summary>
        /// <param name="md"> the model</param>
        /// <param name="ich">the client handler</param>
        public PlayCommand(IModel<Maze> md, IClientHandler ich)
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
                string move = args[0];
                // find the playing game.
                MultiPlayerGame game = model.GetGame(client);
                // get the client we need to send him the close msg.
                TcpClient opp = game.GetOpponent(client);
                if (game == null || opp == null)
                {
                    return new TaskResult(null, false);
                }
                // json solution.
                JObject sol = new JObject
                {
                    { "Name", game.Name },
                    { "Direction", move }
                };
                string Jsol = JsonConvert.SerializeObject(sol);
                // send the opp the players move.
                ch.SendMessage(opp, Jsol);
                // stay connected.
                return new TaskResult("", true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new TaskResult("error occured", false);
            }
        }
    }
}
