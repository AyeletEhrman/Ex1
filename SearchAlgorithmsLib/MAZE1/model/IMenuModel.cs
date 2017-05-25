using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.model
{
    interface IMenuModel
    {
        void SaveSettings();
        int MazeRows { get; set; }
        int MazeCols { get; set; }
    }
}
