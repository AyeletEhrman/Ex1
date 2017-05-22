using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProject;
using System.Net;
using System.ComponentModel;

namespace MazeGUI
{
    class SingleGameModel : /*NotifyChanges,*/ ISingleGameModel
    {
        // has Client
        Client client;
        int mazeRows;
        int mazeCols;
        string mazeName;
        string mazeStr;
        Position initialPos;
        Position goalPos;
        Position currentPos;
        public SingleGameModel()
        {
            // open end poin  connection.
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Properties.Settings.Default.ServerPort);
            client = new Client(ep);
            mazeRows = 0;
            mazeCols = 0;
            mazeName = "";
            mazeStr = "";
            initialPos = new Position(0, 0);
            goalPos = new Position(0, 0);
            currentPos = new Position(0, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public int MazeRows
        {
            get { return mazeRows; }
            set
            {
                mazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return mazeCols; }
            set
            {
                mazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        public string MazeName
        {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        public string MazeStr
        {
            get { return mazeStr;/*"1,0,0,0,0,0,1,0,1,1,1,1,0,0,0,1,0,0,0,1,1,0,1,1,0"*/ }
            set
            {
                mazeStr = value;
                NotifyPropertyChanged("MazeStr");
            }
        }
        public Position InitialPos
        {
            get { return initialPos; }
            set
            {
                initialPos = value;
                NotifyPropertyChanged("InitialPos");
            }
        }
        public Position GoalPos
        {
            get { return goalPos; }
            set
            {
                goalPos = value;
                NotifyPropertyChanged("GoalPos");
            }
        }
        public Position CurrentPos
        {
            get { return currentPos; }
            set
            {
                currentPos = value;
                NotifyPropertyChanged("CurrentPos");
            }
        }
        public void Generate(string name, string rows, string cols)
        {
            string mazeJs;
            mazeJs = client.Send("generate" + " " + name + " " + rows + " " + cols);
            Maze maze = Maze.FromJSON(mazeJs);
            MazeName = maze.Name;
            MazeRows = maze.Rows;
            MazeCols = maze.Cols;
            MazeStr = maze.ToString();
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
            CurrentPos = InitialPos;
        }
    }
}
