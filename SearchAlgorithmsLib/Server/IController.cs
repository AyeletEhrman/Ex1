﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    interface IController
    {
        string ExecuteCommand(string commandLine, TcpClient client);
     //   void NotifyGenerated(string name);
    }
}
