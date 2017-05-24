using MazeGUI.model;
using MazeGUI.viewmodel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MultiGameMenu.xaml
    /// </summary>
    public partial class MultiGameMenu : Window
    {
        private MultiMenuViewModel svm;
        public MultiGameMenu()
        {
            InitializeComponent();
            IMultiMenuModel sm = new MultiMenuModel();
            svm = new MultiMenuViewModel(sm);
            this.DataContext = svm;
            mazeInfo.btnStart.Click += delegate (Object sender, RoutedEventArgs e)
            {
                // writing task.
                //new Task(() =>
                //{
                MultiGameWindow mgw = new MultiGameWindow();
                mgw.Owner = this;
                // spw.Generate(name, rows, cols);
                int retVal = mgw.Generate(mazeInfo.txtMazeName.Text,
                                          mazeInfo.txtRows.Text,
                                          mazeInfo.txtCols.Text);
                if (retVal < 0)
                {
                    /*Application.Current.MainWindow.Show();
                    //this.Close();//??
                    this.Hide();*/
                }

                //}).Start();
                ///????????????????????????????????????????????????????????????????????
                //this.Close();
            };

        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}