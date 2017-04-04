using System;
using System.Collections.Generic;

using System.Text;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class MazeSearchable : ISearchable<Position>
    {
        private Maze maze;

        public State<Position> getInitialState() { return new State<Position>(maze.InitialPos, 0); }
        public State<Position> getGoalState() { return null; }
        public List<State<Position>> getAllPossibleStates(State<Position> s) { return null; }
    }
}
