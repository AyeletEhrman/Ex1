using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    class SingleGameViewModel : NotifyChanges
    {
        private ISingleGameModel model;

        public SingleGameViewModel(ISingleGameModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                                         NotifyPropertyChanged("VM_" + e.PropertyName);
                                     };
        }

      /*  public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }*/

        public int VM_MazeRows
        {
            get { return model.MazeRows; }
         /*   set
            {
                model.MazeRows = value;
               // NotifyPropertyChanged("VM_MazeRows");
            }*/
        }

        public int VM_MazeCols
        {
            get { return model.MazeCols; }
           /* set
            {
                model.MazeCols = value;
                //NotifyPropertyChanged("MazeCols");
            }*/
        }
        public string VM_MazeName
        {
            get { return model.MazeName; }
           /* set
            {
                model.MazeName = value;
                //NotifyPropertyChanged("Name");
            }*/
        }
        public string VM_MazeStr
        {
            get { return model.MazeStr; }
          /*  set
            {
                model.MazeStr = value;
               // NotifyPropertyChanged("VM_MazeStr");
            }*/
        }
        public Position VM_InitialPos
        {
            get { return model.InitialPos; }
          /*  set
            {
                model.InitialPos = value;
                //NotifyPropertyChanged("InitialPos");
            }*/
        }
        public Position VM_GoalPos
        {
            get { return model.GoalPos; }
           /* set
            {
                model.GoalPos = value;
                //NotifyPropertyChanged("GoalPos");
            }*/
        }
        public Position VM_CurrentPos
        {
            get { return model.CurrentPos; }
        /*    set
            {
                model.CurrentPos = value;
                //NotifyPropertyChanged("VM_CurrentPos");
            }*/
        }
        public void Generate(string name, string rows, string cols)
        {
            model.Generate(name, rows, cols);
        }
    }
}
