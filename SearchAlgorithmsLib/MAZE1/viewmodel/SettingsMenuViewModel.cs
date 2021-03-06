﻿using MazeGUI.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.viewmodel
{
   class SettingsMenuViewModel : NotifyChanges
    {
        private ISettingsMenuModel model;
        public SettingsMenuViewModel(ISettingsMenuModel model) {
            this.model = model;
        }
        public string ServerIP {
            get { return model.ServerIP; }
            set {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        public int ServerPort {
            get { return model.ServerPort; }
            set {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }
        
        public int MazeRows {
            get { return model.MazeRows; }
            set {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
        public int MazeCols {
            get { return model.MazeCols; }
            set {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        public int SearchAlgorithm {
            get { return model.SearchAlgorithm; }
            set {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
