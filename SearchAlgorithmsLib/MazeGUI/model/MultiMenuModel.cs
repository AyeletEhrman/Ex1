using ClientProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.model
{
    class MultiMenuModel : IMultiMenuModel
    {
        Client client;

        public MultiMenuModel()
        {
            // open end point connection.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Properties.Settings.Default.ServerPort);
            client = new Client(ep);
        }

        public string Name
        {
            get { return Properties.Settings.Default.Name; }
            set { Properties.Settings.Default.Name = value; }
        }
        /*public string GamesList
        {
            get { return Properties.Settings.Default.GamesList; }
            set { Properties.Settings.Default.GamesList = value; }
        }*/
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { /*Properties.Settings.Default.MazeRows = value;*/ }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { /*Properties.Settings.Default.MazeCols = value;*/ }
        }
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
        public string List()
        {
            client.Send("list");
            return client.Answer;
        }
    }
}

