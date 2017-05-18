using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    class SingleGameModel
    {
        // has Client


        

        public int MazeRows
        {
            get { return MazeRows; }
            set
            {
                MazeRows = value;
            }
        }

        public int MazeCols
        {
            get { return MazeCols; }
            set
            {
                MazeCols = value;
            }
        }
        public string Name
        {
            get { return model.Name; }
            set
            {
                Name = value;
            }
        }
        public string MazeStr
        {
            get { return model.MazeStr; }
            set
            {
                MazeStr = value;
            }
        }
        public Position Initial
        {
            get { return model.Initial; }
            set
            {
                Initial = value;
            }
        }
        public Position Goal
        {
            get { return model.Goal; }
            set
            {
                Goal = value;
            }
        }
        public Position Current
        {
            get { return model.Current; }
            set
            {
                Current = value;
            }
        }
    }
}
