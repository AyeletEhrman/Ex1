using MazeGUI.model;
using MazeGUI.viewmodel;
using MazeGUI.controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SingleGameWindow : Window
    {
        private SingleGameViewModel sgvm;
        public SingleGameWindow()
        {
            InitializeComponent();
            ISingleGameModel sm = new SingleGameModel();
            sgvm = new SingleGameViewModel(sm);
            this.DataContext = sgvm;
            mazeBoard.DataContext = sgvm;
            
        }

        public int Generate(string name, string rows, string cols)
        {
          //  Window w = Application.Current.Windows.
            int retVal = sgvm.Generate(name, rows, cols);
            if (retVal < 0)
            {
                //Application.Current.MainWindow.Show();
                //Owner.Close();//??
                return retVal;
            }
            this.Show();
            mazeBoard.DrawMaze();
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
            bool winner = sgvm.Move(e);
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

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            WarningWindow wrw = new WarningWindow();
            wrw.Owner = this;
            wrw.Show();
            wrw.yesBtn.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                sgvm.Restart();
                wrw.Close();
            };
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            //bool taskFinished = false;
            //new Task(()=>
            //{
                sgvm.Solve();
                //taskFinished = true;

               
           // }).Start();
           // while (!taskFinished)
           // {
              //  Thread.Sleep(1);
           // }
            WinnerWindow ww = new WinnerWindow();
            ww.Owner = this;
            ww.Show();

        }

        

    
    }   
}
