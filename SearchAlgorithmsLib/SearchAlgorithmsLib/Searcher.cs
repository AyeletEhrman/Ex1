using System;
using System.Collections.Generic;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        protected void AddToOpenList(State<T> s)
        {
            openList.Enqueue(s, s.cost);
        }

        protected bool OpenContains(State<T> s)
        {
            return openList.Contains(s);
        }

        protected void UpdatePriority(State<T> s, float cost)
        {
            openList.UpdatePriority(s, cost);
        }

        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count; }
        }
        // ISearcher’s methods:
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        public abstract Solution<State<T>> Search(ISearchable<T> searchable);
    }
}
