using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.CompareSolvers();
        }

        public static void CompareSolvers()
        {
            IMazeGenerator gen = new DFSMazeGenerator();
            Maze maze = gen.Generate(10, 10);
            Console.Write(maze);
            ISearchable<Position> myMaze = new MazeSearchable(maze);
            ISearcher<Position> bfs = new Bfs<Position>();
            bfs.Search(myMaze);
            ISearcher<Position> dfs = new Dfs<Position>();
            dfs.Search(myMaze);
            Console.WriteLine(bfs.GetNumberOfNodesEvaluated());
            Console.WriteLine(dfs.GetNumberOfNodesEvaluated());
            Console.ReadKey();
        }
        
    }
}
