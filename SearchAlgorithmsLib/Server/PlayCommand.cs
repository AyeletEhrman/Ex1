using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    class PlayCommand : ICommand
    {
        private IModel<Maze> model;
        private IClientHandler ch;
        public PlayCommand(IModel<Maze> md, IClientHandler ich)
        {
            model = md;
            ch = ich;
        }
        public TaskResult Execute(string[] args, TcpClient client = null)
        {
            if (args.Length != 1)
            {
                return new TaskResult("bad args", false);
            }
            try
            {
                string move = args[0];
                MultiPlayerGame game = model.GetGame(client);
                TcpClient opp = game.GetOpponent(client);
                if (game == null || opp == null)
                {
                    return new TaskResult(null, false);
                }
                JObject sol = new JObject();
                sol.Add("Name", game.Name);
                sol.Add("Direction", move);
                string Jsol = JsonConvert.SerializeObject(sol);
                ch.SendMessage(opp, Jsol);
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
