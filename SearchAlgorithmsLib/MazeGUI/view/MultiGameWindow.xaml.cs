using MazeGUI.model;
using MazeGUI.viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiGameWindow : Window
    {
        MultiGameViewModel mgvm;
        public MultiGameWindow()
        {
            InitializeComponent();
            IMultiGameModel mgm = new MultiGameModel();
            mgvm = new MultiGameViewModel(mgm);
            this.DataContext = mgvm;
            //mazeBoard.DataContext = mgvm;
        }

        public int Generate(string name, string rows, string cols)
        {
            //  Window w = Application.Current.Windows.
            int retVal = mgvm.Generate(name, rows, cols);
            if (retVal < 0)
            {
                //Application.Current.MainWindow.Show();
                //Owner.Close();//??
                return retVal;
            }
            this.Show();
      //      mazeBoard.DrawMaze();
            Owner.Hide();//??
            return 1;
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
            Owner.Close();//??
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool winner = mgvm.Move(e);
            if (winner)
            {
                WinnerWindow ww = new WinnerWindow();
                ww.Owner = this;
                ww.Show();

            }
            //mazeBoard.DrawMaze();
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {

            WarningWindow wrw = new WarningWindow();
            wrw.Owner = this;
            wrw.Show();
            wrw.yesBtn.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                Application.Current.MainWindow.Show();
                this.Close();
                Owner.Close();//??
            };
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            WarningWindow wrw = new WarningWindow();
            wrw.Owner = this;
            wrw.Show();
            wrw.yesBtn.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                Application.Current.MainWindow.Show();
                this.Close();
                Owner.Close();//??
            };
        }
    }
}
