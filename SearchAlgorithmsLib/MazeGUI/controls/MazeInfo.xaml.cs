﻿using MazeGUI.model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MazeGUI.controls
{
    /// <summary>
    /// Interaction logic for MazeInfo.xaml
    /// </summary>
    public partial class MazeInfo : UserControl
    {
        private SettingsMenuViewModel smvm;
        public MazeInfo()
        {
            InitializeComponent();
            smvm = new SettingsMenuViewModel(new SettingsMenuModel());
            this.DataContext = smvm;
        }
        
    }
}
