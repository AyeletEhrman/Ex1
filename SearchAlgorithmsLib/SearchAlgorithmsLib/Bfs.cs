using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Bfs<T> : Searcher<T>
    {
        public override Solution<State<T>> Search(ISearchable<T> searchable)
        { // Searcher's abstract method overriding
            AddToOpenList(searchable.GetInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> goal = searchable.GetGoalState();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(goal)) {
                    return BackTrace(n);
                }// private method, back traces through the parents
                                        
                
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !OpenContains(s))
                    {
                        s.cost = n.cost + 1;
                        // s.setCameFrom(n); // already done by getSuccessors
                        AddToOpenList(s);
                    }
                    else
                    {//??????? 1/orgCostS
                        if ((n.cost + 1) < s.cost)
                        {
                            if (!OpenContains(s))
                                AddToOpenList(s);
                            else
                                s.cost = n.cost + 1;
                        }
                    }
                }
            }
            return new Solution<State<T>>();//?????????????
        }
        private Solution<State<T>> BackTrace(State<T> st)
        {
            List<State<T>> trace = new List<State<T>>();
            do
            {
                trace.Add(st);
                st = st.cameFrom;
            } while (st != null);
            Solution<State<T>> sol = new Solution<State<T>>();
            sol.SolLst = trace;
            return sol;
        }
       
    }
}