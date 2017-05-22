using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    class SingleMenuViewModel : NotifyChanges
    {
        private ISingleMenuModel model;
        public SingleMenuViewModel(ISingleMenuModel model)
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

        public void Generate(string name, string rows, string cols)
        {
            model.Generate(name, rows, cols);
        }
    }
}