using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class StartGameCommand : ICommand
    {
        public string Execute(string[] args, TcpClient client = null)
        {
            return null;
        }
    }
}
