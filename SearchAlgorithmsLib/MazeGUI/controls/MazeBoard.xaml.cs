using MazeLib;
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
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        public MazeBoard()
        {
            InitializeComponent();
        }
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }
        public string Maze
        {
            get { return (string)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }
        public Position InitialPos
        {
            get { return (Position)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }
        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }
        public Position CurrentPos
        {
            get { return (Position)GetValue(CurrentPosProperty); }
            set { SetValue(CurrentPosProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Rows.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public static readonly DependencyProperty ColsProperty =
           DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public static readonly DependencyProperty MazeProperty =
           DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard));

        public static readonly DependencyProperty MazeNameProperty =
           DependencyProperty.Register("MazeName", typeof(string), typeof(MazeBoard));

        public static readonly DependencyProperty InitialPosProperty =
           DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard));

        public static readonly DependencyProperty GoalPosProperty =
           DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard));

        public static readonly DependencyProperty CurrentPosProperty =
           DependencyProperty.Register("CurrentPos", typeof(Position), typeof(MazeBoard));


    }
}
