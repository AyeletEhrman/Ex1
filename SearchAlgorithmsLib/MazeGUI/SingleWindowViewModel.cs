using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    class SingleWindowViewModel : ViewModel
    {
        SingleGameModel model;

        public SingleWindowViewModel(SingleGameModel model)
        {
            this.model = model;
        }
        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string MazeStr
        {
            get { return model.MazeStr; }
            set
            {
                model.MazeStr = value;
                NotifyPropertyChanged("MazeStr");
            }
        }
        public Position Initial
        {
            get { return model.Initial; }
            set
            {
                model.Initial = value;
                NotifyPropertyChanged("Initial");
            }
        }
        public Position Goal
        {
            get { return model.Goal; }
            set
            {
                model.Goal = value;
                NotifyPropertyChanged("Goal");
            }
        }
        public Position Current
        {
            get { return model.Current; }
            set
            {
                model.Current = value;
                NotifyPropertyChanged("Current");
            }
        }


    }
}
