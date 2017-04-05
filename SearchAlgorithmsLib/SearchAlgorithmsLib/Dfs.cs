using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Dfs<T> : ISearcher<T>
    {
        private int evaluatedNodes;

        public List<State<T>> Search(ISearchable<T> searchable)
        {
            evaluatedNodes = 0;
            Stack <State<T>> stack = new Stack<State<T>>();
            HashSet<State<T>> discovered = new HashSet<State<T>>();
            State<T> goal = searchable.GetGoalState();
            stack.Push(searchable.GetInitialState());

            while (stack.Count() > 0)
            {
                State<T> currState = stack.Pop();
                evaluatedNodes++;
                if (currState.Equals(goal))
                {
                    return BackTrace(currState);
                }
                if (!discovered.Contains(currState))
                {
                    discovered.Add(currState);
                    // calling the delegated method, returns a list of states with n as a parent
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(currState);
                    foreach (State<T> s in succerssors)
                    {
                        stack.Push(s);
                    }
                }
            }
            return new List<State<T>>();
        }
        // get how many nodes were evaluated by the algorithm
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        private List<State<T>> BackTrace(State<T> st)
        {
            List<State<T>> trace = new List<State<T>>();
            do
            {
                trace.Add(st);
                st = st.cameFrom;
            } while (st != null);

            return trace;
        }
    }
}
