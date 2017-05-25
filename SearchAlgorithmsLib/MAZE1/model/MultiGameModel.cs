using ClientProject;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MazeGUI.model
{
    class MultiGameModel : GameModel, IMultiGameModel
    {
        Client client;
        int mazeRows;
        int mazeCols;
        string mazeName;
        string mazeStr;
        Position initialPos;
        Position goalPos;
        Position currentPos;
        Maze maze;

        public MultiGameModel()
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
            get { return mazeStr; }
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
        public int Start(string name, string rows, string cols)
        {
            string mazeJs;
            mazeJs = client.Send("start" + " " + name + " " + rows + " " + cols);
            if (mazeJs.Equals("bad args"))
            {
                return -1;
            }
            maze = Maze.FromJSON(mazeJs);
            MazeName = maze.Name;
            MazeRows = maze.Rows;
            MazeCols = maze.Cols;
            MazeStr = maze.ToString();
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
            CurrentPos = InitialPos;
            return 1;
        }

        public void Join(string name)
        {
            client.Send("join" + name);
        }

        public bool Move(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                default:
                    break;
            }
            // if the player won.
            if ((CurrentPos.Row == GoalPos.Row) && (CurrentPos.Col == GoalPos.Col))
            {
                return true;
            }
            return false;
        }
        public void MoveUp()
        {
            if (CurrentPos.Row != 0)
            {
                if (maze[CurrentPos.Row - 1, CurrentPos.Col] == 0)
                {
                    Position pos = new Position(CurrentPos.Row - 1, currentPos.Col);
                    CurrentPos = pos;
                }
            }
        }
        public void MoveDown()
        {
            if (CurrentPos.Row != (mazeRows - 1))
            {
                if (maze[CurrentPos.Row + 1, CurrentPos.Col] == 0)
                {
                    Position pos = new Position(CurrentPos.Row + 1, CurrentPos.Col);
                    CurrentPos = pos;
                }
            }
        }
        public void MoveLeft()
        {
            if (CurrentPos.Col != 0)
            {
                if (maze[CurrentPos.Row, CurrentPos.Col - 1] == 0)
                {
                    Position pos = new Position(CurrentPos.Row, CurrentPos.Col - 1);
                    CurrentPos = pos;
                }
            }
        }
        public void MoveRight()
        {
            if (CurrentPos.Col != (MazeCols - 1))
            {
                if (maze[CurrentPos.Row, CurrentPos.Col + 1] == 0)
                {
                    Position pos = new Position(CurrentPos.Row, CurrentPos.Col + 1);
                    CurrentPos = pos;
                }
            }
        }

    }
}
