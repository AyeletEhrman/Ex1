using MazeLib;
using System;

namespace ServerProject
{
    /// <summary>
    /// runs the server.
    /// </summary>
    class ServerMain
    {
        static void Main(string[] args)
        {
            // the view of the MVC.
            IClientHandler view = new ClientHandler();
            // the model of the MVC.
            IModel<Maze> model = new MazeModel();
            // the controller og the MVC.
            IController controller = new MazeController(model, view);
            view.SetController(controller);
            model.SetController(controller);
            // open server.
            Server server = new Server(1116, view);
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
    }
}
