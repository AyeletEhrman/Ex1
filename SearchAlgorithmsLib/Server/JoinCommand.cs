using MazeLib;
using System;
using System.Net.Sockets;

namespace ServerProject
{
    class JoinCommand : ICommand
    {
        private IModel<Maze> model;
        public JoinCommand(IModel<Maze> model)
        {
            this.model = model;
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
               
                Maze maze = model.Join(client, name);
                if (maze == null)
                {
                    return new TaskResult(null, false);
                }
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
