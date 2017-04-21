using MazeLib;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    class SolveMazeCommand : ICommand
    { 
        private IModel<Maze> model;
        public SolveMazeCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public TaskResult Execute(string[] args, TcpClient client)
        {
            if (args.Length != 2)
            {
                return new TaskResult("bad args", false);
            }
            try { 
                string name = args[0];
                int algo = int.Parse(args[1]);
                string solution = model.Solve(name, algo);
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
