using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerProject
{
    interface ICommand
    {
        string Execute(string[] args, TcpClient client = null);
        //Some commands need the TcpClient object to send messages back to the client
    }
}
