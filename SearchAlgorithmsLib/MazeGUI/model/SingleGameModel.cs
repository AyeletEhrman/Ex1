using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientProject;
using System.Net;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MazeGUI.model
{
    class SingleGameModel : NotifyChanges, ISingleGameModel
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

        /*public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }*/
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
        public int Generate(string name, string rows, string cols)
        {
            string mazeJs;
            mazeJs = client.Send("generate" + " " + name + " " + rows + " " + cols);
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
        public void Restart()
        {
            Position pos = new Position(InitialPos.Row, InitialPos.Col);
            CurrentPos = pos;
        }
        public void Solve()
        {

            string solution;
          //  bool sleep = true;
            solution = client.Send("solve" + " " + MazeName + " " +
                                   Properties.Settings.Default.SearchAlgorithm);
            string[] solutionFields = solution.Split(',');
            solution = solutionFields[1];
            solution = solution.Replace("\"Solution\":", "");
            solution = solution.Replace("\"", "");
            solution = solution.Replace(" ", "");

            for (int i = 0; i < solution.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background, new Action(() =>
                    {
                        switch (solution[i])
                        {
                            case '0':
                                MoveLeft();
                                break;
                            case '1':
                                MoveRight();
                                break;
                            case '2':
                                MoveUp();
                                break;
                            case '3':
                                MoveDown();
                                break;
                            case 'N':
                                return;
                            default:
                                break;
                        }
                        Thread.Sleep(200);
                    }));
            }
        }
    }
}
