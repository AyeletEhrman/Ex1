using MazeLib;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    class CloseCommand : ICommand
    {
        private IModel<Maze> model;
        private IClientHandler ch;
        public CloseCommand(IModel<Maze> md, IClientHandler ich)
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
                string name = args[0];
                MultiPlayerGame game = model.GetGame(client);
                TcpClient opp = game.GetOpponent(client);
                if (game == null || opp == null)
                {
                    return new TaskResult(null, false);
                }
                ch.SendMessage(opp, "game over");
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
