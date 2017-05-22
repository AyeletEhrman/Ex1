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

namespace MazeGUI
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
                    smvm.Generate(mazeInfo.txtMazeName.Text,
                                  mazeInfo.txtRows.Text,
                                  mazeInfo.txtCols.Text);
                //}).Start();
                ///????????????????????????????????????????????????????????????????????
                //this.Close();
            };
            
        }
        
    }
}
