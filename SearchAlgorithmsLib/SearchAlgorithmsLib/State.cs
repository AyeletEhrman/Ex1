using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        public T state { get; set; } // the state represented by a string
        public double cost { get; set; } // cost to reach this state (set by a setter)
        public State<T> cameFrom { get; set; } // the state we came from to this state (setter)

        public State(T state, double cost) // CTOR
        {
            this.state = state;
            this.cost = cost;
        }

        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }

        public override int GetHashCode()
        {
            return (state.ToString() + cost.ToString()).GetHashCode();
        }
        // ...
    }
}
