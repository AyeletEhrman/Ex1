using MazeLib;
using System.Net.Sockets;

namespace ServerProject
{
    class ShowListCommand : ICommand
    {
        private IModel<Maze> model;
        public ShowListCommand(IModel<Maze> model)
        {
            this.model = model;
        }
        public TaskResult Execute(string[] args, TcpClient client = null)
        {
            return new TaskResult(model.List(), false);
        }
    }
}
