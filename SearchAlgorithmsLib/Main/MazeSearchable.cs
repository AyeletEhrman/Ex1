using System;
using System.Collections.Generic;

using MazeLib;
using SearchAlgorithmsLib;
namespace Main
{
    public class MazeSearchable : ISearchable<Position>
    {
        private Maze maze;

        public MazeSearchable(Maze maze)
        {
            this.maze = maze;
        }

        public State<Position> GetInitialState()
        {
            return new State<Position>(maze.InitialPos);
        }

        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.GoalPos);
        }
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> posSt = new List<State<Position>>();
            if (s.state.Row + 1 < maze.Rows)
            {
                if (maze[s.state.Row + 1, s.state.Col] == 0)
                {
                    State<Position> st = new State<Position>(new Position(s.state.Row + 1, s.state.Col));
                    st.cameFrom = s;
                    posSt.Add(st);
                }
            }
            if (s.state.Row - 1 >= 0)
            {
                if (maze[s.state.Row - 1, s.state.Col] == 0)
                {
                    State<Position> st = new State<Position>(new Position(s.state.Row - 1, s.state.Col));
                    st.cameFrom = s;
                    posSt.Add(st);
                }
            }
            if (s.state.Col + 1 < maze.Cols)
            {
                if (maze[s.state.Row, s.state.Col + 1] == 0)
                {
                    State<Position> st = new State<Position>(new Position(s.state.Row, s.state.Col + 1));
                    st.cameFrom = s;
                    posSt.Add(st);
                }
            }
            if (s.state.Col - 1 >= 0)
            {
                if (maze[s.state.Row, s.state.Col - 1] == 0)
                {
                    State<Position> st = new State<Position>(new Position(s.state.Row, s.state.Col - 1));
                 
                    st.cameFrom = s;
                    posSt.Add(st);
                }
            }
            return posSt;
        }
    }
}
