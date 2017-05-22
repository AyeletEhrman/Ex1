using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    class MultiMenuModel : IMultiMenuModel
    {
        public string Name
        {
            get { return Properties.Settings.Default.Name; }
            set { Properties.Settings.Default.Name = value; }
        }
        public string GamesList
        {
            get { return Properties.Settings.Default.GamesList; }
            set { Properties.Settings.Default.GamesList = value; }
        }
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
    }
}

