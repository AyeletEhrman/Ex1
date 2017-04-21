using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class TaskResult
    {
        public string JsonSol { get; set; }
        public bool StayConnected { get; set; }
        public TaskResult(string Jsol, bool sc)
        {
            JsonSol = Jsol;
            StayConnected = sc;
        }
    }
}
