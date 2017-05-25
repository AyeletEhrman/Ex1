using MazeLib;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGUI.controls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        Position lastPos;
        public MazeBoard()
        {
            InitializeComponent();
        }

        // Using a DependencyProperty as the backing store for Rows.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public static readonly DependencyProperty ColsProperty =
           DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));

        public static readonly DependencyProperty MazeProperty =
           DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard));//, new PropertyMetadata(null, new PropertyChangedCallback(OnMazePropertyChanged)));

        public static readonly DependencyProperty MazeNameProperty =
           DependencyProperty.Register("MazeName", typeof(string), typeof(MazeBoard));

        public static readonly DependencyProperty InitialPosProperty =
           DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard));

        public static readonly DependencyProperty GoalPosProperty =
           DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard));

        public static readonly DependencyProperty CurrentPosProperty =
           DependencyProperty.Register("CurrentPos", typeof(Position), typeof(MazeBoard),
               new PropertyMetadata(new Position(), new PropertyChangedCallback(OnCurrPosPropertyChanged)));

        private static void OnCurrPosPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).ChangeCurrentPos();
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

        private static void OnMazePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).DrawMaze();
        }



        public void DrawMaze()
        {
            while (Maze == "")
            {
                Thread.Sleep(5);
            }
            ImageBrush initBrush = new ImageBrush(new BitmapImage(new Uri("../../resources/monsters.jpg", UriKind.Relative)));////////////?????????
            Rectangle initRec = new Rectangle();
            initRec.Height = myCanvas.ActualHeight / Rows;
            initRec.Width = myCanvas.ActualWidth / Cols;
            initRec.Fill = initBrush;
            Canvas.SetLeft(initRec, InitialPos.Col * initRec.Width);
            Canvas.SetTop(initRec, InitialPos.Row * initRec.Height);
            myCanvas.Children.Add(initRec);
            lastPos = new Position(InitialPos.Row, InitialPos.Col);

            ImageBrush goalBrush = new ImageBrush(new BitmapImage(new Uri("../../resources/Boo.png", UriKind.Relative)));////////////?????????
            Rectangle goalRec = new Rectangle();
            goalRec.Height = myCanvas.ActualHeight / Rows;
            goalRec.Width = myCanvas.ActualWidth / Cols;
            goalRec.Fill = goalBrush;
            Canvas.SetLeft(goalRec, GoalPos.Col * goalRec.Width);
            Canvas.SetTop(goalRec, GoalPos.Row * goalRec.Height);
            myCanvas.Children.Add(goalRec);
            
            Maze = Maze.Replace("\r\n", "");

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Maze[i * Cols + j] == '1')
                    {
                        Rectangle rec = new Rectangle();
                        //ImageBrush recBrush = new ImageBrush(new BitmapImage(new Uri("../../resources/door.jpg", UriKind.Relative)));///?????????????????????????????????
                        rec.Height = myCanvas.ActualHeight / Rows;
                        rec.Width = myCanvas.ActualWidth / Cols;
                       // rec.Fill = recBrush;
                        rec.Fill = new SolidColorBrush(Colors.Black);
                      //  rec.Fill = new Bord(Colors.Black);
                        Canvas.SetLeft(rec, j * rec.Width);
                        Canvas.SetTop(rec, i * rec.Height);
                        myCanvas.Children.Add(rec);
                    }
                }
            }
          //  }).Start();
        }
        public void ChangeCurrentPos()
        {
            Rectangle rec = new Rectangle();
            rec.Height = myCanvas.ActualHeight / Rows;
            rec.Width = myCanvas.ActualWidth / Cols;
            rec.Fill = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(rec, lastPos.Col * rec.Width);
            Canvas.SetTop(rec, lastPos.Row * rec.Height);
            myCanvas.Children.Add(rec);


            ImageBrush currBrush = new ImageBrush(new BitmapImage(new Uri("../../resources/monsters.jpg", UriKind.Relative)));////////////?????????
            Rectangle currRec = new Rectangle();
            currRec.Height = myCanvas.ActualHeight / Rows;
            currRec.Width = myCanvas.ActualWidth / Cols;
            currRec.Fill = currBrush;
            Canvas.SetLeft(currRec, CurrentPos.Col * currRec.Width);
            Canvas.SetTop(currRec, CurrentPos.Row * currRec.Height);
            myCanvas.Children.Add(currRec);
            lastPos = new Position(CurrentPos.Row, CurrentPos.Col);

        }
    }
}
