using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MazeGUI.model
{
    interface IMultiGameModel : INotifyPropertyChanged
    {
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string MazeName { get; set; }
        string MazeStr { get; set; }
        Position InitialPos { get; set; }
        Position GoalPos { get; set; }
        Position CurrentPos { get; set; }
        Position OpponentPos { get; set; }

        int Start(string name, string rows, string cols);
        void Join(string name);
        bool Play();
        void Close();

        bool Move(KeyEventArgs e);
    }
}
