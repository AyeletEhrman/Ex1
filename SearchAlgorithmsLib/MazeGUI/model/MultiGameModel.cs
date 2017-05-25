using ClientProject;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        Position opponentPos;
        Maze maze;
        bool sendClose;
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
            opponentPos = new Position(0, 0);
            sendClose = true;
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
        public Position OpponentPos
        {
            get { return opponentPos; }
            set
            {
                opponentPos = value;
                NotifyPropertyChanged("OpponentPos");
               // NotifyPropertyChanged("CurrentPos");
            }
        }
        public int Start(string name, string rows, string cols)
        {
            new Task(() =>
            {
                client.Send("start" + " " + name + " " + rows + " " + cols);
            }).Start();

            while (!client.NextCommand)
            {
                Thread.Sleep(100);
            }
            client.NextCommand = false;

            string mazeJs = client.Answer;

            if (mazeJs.Equals("bad args"))
            {
                return -1;
            }
            UpdateProperties(mazeJs);
            return 1;
        }

        public void Join(string name)
        {
            new Task(() =>
            {
                client.Send("join" + " " + name);
            }).Start();

            while(!client.NextCommand)
            {
                Thread.Sleep(100);
            }
            client.NextCommand = false;

            string mazeJs = client.Answer;
            UpdateProperties(mazeJs);
        }

        public bool Play()
        {
            Console.WriteLine("in play");
            while (client.Answer != "game over")
            {
                if (client.Answer.Equals("multi") || client.Answer.Equals("game over"))///////
                {
                    continue;
                }
                if (client.NextCommand)
                {
                    bool lost = MoveOpponnent(client.Answer);
                    if(lost)
                    {
                        return false;
                    }
                    client.NextCommand = false;
                }

                Thread.Sleep(100);
                Console.WriteLine("in loop");
            }

        //    MessageBox.Show("Opponent exited \n GAME OVER");
            Console.WriteLine("hello from client");
            sendClose = false;
            return true;
        }

        public void Close()
        {
            if (sendClose)
            {
                client.CommandLine = "close" + " " + MazeName;
                client.CommandReady = true;
            }
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
                    Position posTemp = new Position(CurrentPos.Row - 1, CurrentPos.Col);
                    CurrentPos = posTemp;
                    client.CommandLine = "play up";
                    client.CommandReady = true;
                }
            }
        }
        public void MoveDown()
        {
            if (CurrentPos.Row != (mazeRows - 1))
            {
                if (maze[CurrentPos.Row + 1, CurrentPos.Col] == 0)
                {
                    Position posTemp = new Position(CurrentPos.Row + 1, CurrentPos.Col);
                    CurrentPos = posTemp;
                    client.CommandLine = "play down";
                    client.CommandReady = true;
                }
            }
        }
        public void MoveLeft()
        {
            if (CurrentPos.Col != 0)
            {
                if (maze[CurrentPos.Row, CurrentPos.Col - 1] == 0)
                {
                    Position posTemp = new Position(CurrentPos.Row, CurrentPos.Col - 1);
                    CurrentPos = posTemp;
                    client.CommandLine = "play left";
                    client.CommandReady = true;
                }
            }
        }
        public void MoveRight()
        {
            if (CurrentPos.Col != (MazeCols - 1))
            {
                if (maze[CurrentPos.Row, CurrentPos.Col + 1] == 0)
                {
                    Position posTemp = new Position(CurrentPos.Row, CurrentPos.Col + 1);
                    CurrentPos = posTemp;
                    client.CommandLine = "play right";
                    client.CommandReady = true;
                }
            }
        }
        private void UpdateProperties(string mazeJs)
        {
            maze = Maze.FromJSON(mazeJs);
            MazeName = maze.Name;
            MazeRows = maze.Rows;
            MazeCols = maze.Cols;
            MazeStr = maze.ToString();
            InitialPos = maze.InitialPos;
            GoalPos = maze.GoalPos;
            CurrentPos = InitialPos;
            OpponentPos = InitialPos;
        }
        public bool MoveOpponnent(string oppMove)
        {
            string[] oppMoveArr = oppMove.Split(',');
            oppMove = oppMoveArr[1];
            oppMove = oppMove.Replace("\"", "");
            oppMove = oppMove.Replace("}", "");
            oppMove = oppMove.Replace(" ", "");
            oppMove = oppMove.Replace("Direction:", "");

            Position posTemp;
            switch (oppMove)
            {
                case "up":
                    posTemp = new Position(OpponentPos.Row - 1, OpponentPos.Col);
                    OpponentPos = posTemp;
                    break;
                case "down":
                    posTemp = new Position(OpponentPos.Row + 1, OpponentPos.Col);
                    OpponentPos = posTemp;
                    break;
                case "right":
                    posTemp = new Position(OpponentPos.Row, OpponentPos.Col + 1);
                    OpponentPos = posTemp;
                    break;
                case "left":
                    posTemp = new Position(OpponentPos.Row, OpponentPos.Col - 1);
                    OpponentPos = posTemp;
                    break;
                default:
                    break;
            }
            // if the player won.
            if ((OpponentPos.Row == GoalPos.Row) && (OpponentPos.Col == GoalPos.Col))
            {
                return true;
            }
            return false;
        }
    }
}
