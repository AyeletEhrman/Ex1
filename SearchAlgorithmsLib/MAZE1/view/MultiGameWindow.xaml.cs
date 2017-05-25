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
    /// Interaction logic for MultiGameWindow.xaml
    /// </summary>
    public partial class MultiGameWindow : Window
    {
        bool startGame = false;
        MultiGameViewModel mgvm;
        public MultiGameWindow()
        {
            InitializeComponent();
            IMultiGameModel mgm = new MultiGameModel();
            mgvm = new MultiGameViewModel(mgm);
            this.DataContext = mgvm;
            myBoard.DataContext = mgvm;
        }

        public int Start(string name, string rows, string cols)
        {
            //loadGif.Play();
            this.Show();
            Owner.Hide();//??
            loadGif.Play();
            //  Window w = Application.Current.Windows.
            int retVal = mgvm.Start(name, rows, cols);
            if (retVal < 0)
            {
                //Application.Current.MainWindow.Show();
                //Owner.Close();//??
                return retVal;
            }
            loadGif.Stop();
            myBoard.DrawMaze();
            otherBoard.DrawMaze();
            while (!startGame)
            {
                loadGif.Play();
               // grid
            }
            //this.g

            
            return 1;
        }

        public void Join(string name)
        {
            mgvm.Join(name);
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
