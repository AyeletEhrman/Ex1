using MazeGUI.model;
using MazeGUI.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for MultiGameMenu.xaml
    /// </summary>
    public partial class MultiGameMenu : Window
    {
        MultiMenuViewModel mmvm;
        ObservableCollection<string> gamesList = new ObservableCollection<string>();
        MultiGameWindow mgw;
        public MultiGameMenu()
        {
            InitializeComponent();
            mgw = new MultiGameWindow();
            
            IMultiMenuModel mm = new MultiMenuModel();
            mmvm = new MultiMenuViewModel(mm);
            this.DataContext = mmvm;
            mazeInfo.btnStart.Click += delegate (Object sender, RoutedEventArgs e)
            {
                mgw.Owner = this;
                int retVal = mgw.Start(mazeInfo.txtMazeName.Text,
                                          mazeInfo.txtRows.Text,
                                          mazeInfo.txtCols.Text);
                if (retVal < 0)
                {
                    /*Application.Current.MainWindow.Show();
                    //this.Close();//??
                    this.Hide();*/
                }
                //this.Close();
                //Thread.Sleep(1000);
                
            };
            
            games.ItemsSource = gamesList;            string gamesLst = mmvm.List();
            if (!gamesLst.Equals("[]"))
            {
                gamesLst = gamesLst.Replace("\r\n", "");
                gamesLst = gamesLst.Replace("[", "");
                gamesLst = gamesLst.Replace("]", "");
                gamesLst = gamesLst.Replace(" ", "");
                gamesLst = gamesLst.Replace("\"", "");
                string[] gamesArr = gamesLst.Split(',');

                for (int i = 0; i < gamesArr.Length; i++)
                {
                    //gamesArr[i] = gamesArr[i].Replace("\"", "");

                    gamesList.Add(gamesArr[i]);

                }
            }
        }

        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            mgw.Join(gamesList[games.SelectedIndex]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}