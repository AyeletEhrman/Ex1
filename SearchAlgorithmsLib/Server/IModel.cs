using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject
{
    interface IModel<T>
    {
        void SetController(IController cont);
        T Generate(string name, int x, int y);
     //   T GetGeneratedObj();
        string Solve(string name, int searcher);
    }
}
