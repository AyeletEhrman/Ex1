﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void SingleBtn_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerMenu menu = new SinglePlayerMenu();
            menu.Owner = this;
            
            menu.ShowDialog();
        }

        private void MultiBtn_Click(object sender, RoutedEventArgs e)
        {
            MultiPlayerMenu menu = new MultiPlayerMenu();
            menu.Owner = this;
            menu.ShowDialog();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenu menu = new SettingsMenu();
            menu.Owner = this;
            menu.ShowDialog();
        }
    }
}
