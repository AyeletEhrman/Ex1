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
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SingleGameWindow : Window
    {
        private SingleGameViewModel sgvm;
        public SingleGameWindow()//(SingleGameViewModel vm)
        {
            InitializeComponent();
            ISingleGameModel sm = new SingleGameModel();
            sgvm = new SingleGameViewModel(sm);
            this.DataContext = sgvm;
            mazeBoard.DataContext = sgvm;
        }

        public void Generate(string name, string rows, string cols)
        {
            this.Show();
            sgvm.Generate(name, rows, cols);
            mazeBoard.DrawMaze();
            
        }

        private void mazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            // mazeBoard.DrawMaze();
        }
    }   
}
