using MazeGUI.model;
using MazeGUI.viewmodel;
using System.Windows;

namespace MazeGUI.view
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window
    {
        private SettingsMenuViewModel vm;
        public SettingsMenu()
        {
            InitializeComponent();
            ISettingsMenuModel sm = new SettingsMenuModel();
            vm = new SettingsMenuViewModel(sm);
            this.DataContext = vm;
        }
        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
