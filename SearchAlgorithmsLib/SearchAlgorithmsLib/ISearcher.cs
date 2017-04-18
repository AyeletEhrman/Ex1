using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public interface ISearcher<T>
    {
        // the search method
        // Solution?
        Solution<State<T>> Search(ISearchable<T> searchable);
        // get how many nodes were evaluated by the algorithm
        int GetNumberOfNodesEvaluated();
    }
}
