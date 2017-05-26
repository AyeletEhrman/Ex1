using MazeGUI.model;
using MazeGUI.viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for MultiGameWindow.xaml
    /// </summary>
    public partial class MultiGameWindow : Window
    {
        //bool startGame = false;
        MultiGameViewModel mgvm;
        public bool ToClose { get; set; }
        public MultiGameWindow()
        {
            InitializeComponent();
            IMultiGameModel mgm = new MultiGameModel();
            mgvm = new MultiGameViewModel(mgm);
            this.DataContext = mgvm;
            myBoard.DataContext = mgvm;
            otherBoard.DataContext = mgvm;
            ToClose = false;
        }

        public int Start(string name, string rows, string cols)
        {
            //loadGif.Play();
            this.Show();
            Owner.Hide();//??
                         //  loadGif.Play();
                         //  Window w = Application.Current.Windows.
                         
            int retVal = mgvm.Start(name, rows, cols);
            if (retVal < 0)
            {
                //Application.Current.MainWindow.Show();
                //Owner.Close();//??
                return retVal;
            }
            waitingTxt.Visibility = Visibility.Hidden;
           // waitingTxt.Text = "";
            myBoard.DrawMaze();
            otherBoard.DrawMaze();
           // otherBoard.CurrentPos = mgvm.VM_InitialPos;

            /*while (!startGame)
            {
                loadGif.Play();
               // grid
            }*/
            //this.g
            new Task(() =>
            {
                bool didntLoose = mgvm.Play();
                Console.WriteLine("after start PLAY");
                ToClose = true;
                if (didntLoose) {
                    CloseWindow();
                }
                else
                {
                    LostWindow();
                }
                //this.Close();
            }).Start();
        /*    while (!toClose)
            {
                Thread.Sleep(100);
            }
            this.Close();*/
            return 1;
        }

        public void Join(string name)
        {
            mgvm.Join(name);


            this.Show();
            Owner.Hide();//??
            waitingTxt.Visibility = Visibility.Hidden;
            myBoard.DrawMaze();
            otherBoard.DrawMaze();
            new Task(() =>
            {
                bool didntLoose = mgvm.Play();
                ToClose = true;
                Console.WriteLine("after join PLAY");

                if (didntLoose)
                {
                    CloseWindow();
                } 
                else
                {
                    LostWindow();
                }
                //this.Close();
            }).Start();
         /*   while (!toClose)
            {
                Thread.Sleep(100);
            }
            this.Close();*/
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
            mgvm.Close();
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
        
        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            WarningWindow wrw = new WarningWindow();
            wrw.Owner = this;
            wrw.Show();
            wrw.yesBtn.Click += delegate (object sender1, RoutedEventArgs e1)
            {
                this.Close();
            };
        }
        private void CloseWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                
        ///        this.AddChild(tb);
                
                this.Close();
            });   
        }
        private void LostWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                LooserWindow lw = new LooserWindow();
                lw.Owner = this;
                lw.Show();
            });
        }
       
    }
}
