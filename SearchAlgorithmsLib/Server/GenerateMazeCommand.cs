using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class GenerateMazeCommand : ICommand
    {
        private IModel<Maze> model;
        public GenerateMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 3)
            {
                return "bad args";
            }
            try
            {
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                Maze maze = model.Generate(name, rows, cols);
                return maze.ToJSON();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "error occured";
            }
        }
    }
}
