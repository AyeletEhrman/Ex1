using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
