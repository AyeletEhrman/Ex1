using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server
{
    class MazeModel : IModel<Maze>
    {
        public Maze Generate(int x, int y)
        {
            return new Maze(x, y);
        }
        public string Solve(Maze maze, int searcher)
        {
            return null;
        }
    }
}
