using System;
using System.Collections.Generic;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}