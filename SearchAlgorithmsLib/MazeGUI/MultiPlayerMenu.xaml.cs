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
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window
    {
        private MultiMenuViewModel svm;
        public MultiPlayerMenu()
        {
            InitializeComponent();
            IMultiMenuModel sm = new MultiMenuModel();
            svm = new MultiMenuViewModel(sm);
            this.DataContext = svm;
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}