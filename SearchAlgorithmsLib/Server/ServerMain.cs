using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            IClientHandler view = new ClientHandler();
            IModel<Maze> model = new MazeModel();
            IController controller = new MazeController(model, view);
            view.SetController(controller);
            model.SetController(controller);
            Server server = new Server(1116, view);
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
    }
}
