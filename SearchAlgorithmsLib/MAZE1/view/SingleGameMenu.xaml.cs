using MazeGUI.model;
using MazeGUI.viewmodel;
using System;
using System.Windows;

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SingleGameMenu : Window
    {
        private SingleMenuViewModel smvm;
        public SingleGameMenu()
        {
            InitializeComponent();
            ISingleMenuModel sm = new SingleMenuModel();
            smvm = new SingleMenuViewModel(sm);
            this.DataContext = smvm;

            mazeInfo.btnStart.Click += delegate (Object sender, RoutedEventArgs e)
            {
                // writing task.
                //new Task(() =>
                //{
                SingleGameWindow sgw = new SingleGameWindow();
                sgw.Owner = this;
               // spw.Generate(name, rows, cols);
                int retVal = sgw.Generate(mazeInfo.txtMazeName.Text,
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
