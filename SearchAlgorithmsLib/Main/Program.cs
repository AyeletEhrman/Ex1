using System;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;

namespace Main
{
    /// <summary>
    /// runs the Main project.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Program.CompareSolvers();
        }

        /// <summary>
        /// compares between a bfs and dfs searchers on a maze.
        /// </summary>
        public static void CompareSolvers()
        {
            IMazeGenerator gen = new DFSMazeGenerator();
            // get a random maze size 50X50.
            Maze maze = gen.Generate(50, 50);
            /// print the maze.
            Console.Write(maze);
            // make the maze searchable.
            ISearchable<Position> myMaze = new MazeSearchable(maze);
            // bfs solution.
            ISearcher<Position> bfs = new Bfs<Position>();
            bfs.Search(myMaze);
            // dfs solution.
            ISearcher<Position> dfs = new Dfs<Position>();
            dfs.Search(myMaze);
            // write number of nodes evaluated in each search.
            Console.WriteLine(bfs.GetNumberOfNodesEvaluated());
            Console.WriteLine(dfs.GetNumberOfNodesEvaluated());
            Console.ReadKey();
        }
    }
}