using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IModel<T>
    {
        T Generate(int x, int y);
        string Solve(T problem, int searcher);
    }
}
