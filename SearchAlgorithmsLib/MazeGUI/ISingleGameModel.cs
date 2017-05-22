using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    interface ISingleGameModel : INotifyPropertyChanged
    {

        int MazeRows { get; set; }
        int MazeCols { get; set; }
        string MazeName { get; set; }
        string MazeStr { get; set; }
        Position InitialPos { get; set; }
        Position GoalPos { get; set; }
        Position CurrentPos { get; set; }
        void Generate(string name, string rows, string cols);
    }
}
