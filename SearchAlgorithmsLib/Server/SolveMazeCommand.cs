using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class SolveMazeCommand : ICommand
    { 
        private IModel<Maze> model;
        public SolveMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 2)
            {
                return "bad args";
            }
            try { 
                string name = args[0];
                int algo = int.Parse(args[1]);
                string solution = model.Solve(name, algo);
                return solution;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "error occured";
            }
}
    }
}
