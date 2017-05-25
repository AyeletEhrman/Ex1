using MazeGUI.model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MazeGUI.viewmodel
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

        public int VM_MazeRows
        {
            get { return model.MazeRows; }
        }

        public int VM_MazeCols
        {
            get { return model.MazeCols; }
        }
        public string VM_MazeName
        {
            get { return model.MazeName; }
        }
        public string VM_MazeStr
        {
            get { return model.MazeStr; }
        }
        public Position VM_InitialPos
        {
            get { return model.InitialPos; }
        }
        public Position VM_GoalPos
        {
            get { return model.GoalPos; }
        }
        public Position VM_CurrentPos
        {
            get { return model.CurrentPos; }
        }
        public int Generate(string name, string rows, string cols)
        {
            return model.Generate(name, rows, cols);
        }
        public bool Move(KeyEventArgs e)
        {
            return model.Move(e);
        }
        public void Restart()
        {
            model.Restart();
        }
        public void Solve()
        {
            model.Solve();
        }
    }
}
