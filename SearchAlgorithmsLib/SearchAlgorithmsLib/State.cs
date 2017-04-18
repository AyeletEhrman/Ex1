using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        public T state { get; set; } // the state represented by a string
      //  public float orgCost { get; set; } // original cost
        public float cost { get; set; } // cost to reach this state (set by a setter)
        public State<T> cameFrom { get; set; } // the state we came from to this state (setter)

        public State(T state) // CTOR
        {
            this.state = state;
         //   this.orgCost = orgCost;
            this.cost = 0;
            this.cameFrom = null;
        }

       /* public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }*/
        public override bool Equals(object obj)
        {
            return state.Equals((obj as State<T>).state);
        }

        public override int GetHashCode()
        {
            // return (state.ToString() + cost.ToString()).GetHashCode();
            return state.ToString().GetHashCode();
            //return state.GetHashCode();
        }
        // ...
    } /*   public static class StatePool<T>
    {
        private static HashSet<State<T>> pool = new HashSet<State<T>>();
        private static HashSet<T> posPool = new HashSet<T>();
        public static State<T> getState(T newSt)
        {
            if (pool.Contains()) { }

            return pool.get(newSt);
        }
    }*/
}
