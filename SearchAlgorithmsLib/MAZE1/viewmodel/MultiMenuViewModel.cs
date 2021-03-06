﻿using MazeGUI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.viewmodel
{
    class MultiMenuViewModel : NotifyChanges
    {
        private IMultiMenuModel model;
        public MultiMenuViewModel(IMultiMenuModel model)
        {
            this.model = model;
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
        /*public string GamesList
        {
            get { return model.GamesList; }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("GamesList");
            }
        }*/
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

        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public string List()
        {
            return model.List();
        }
    }
}
