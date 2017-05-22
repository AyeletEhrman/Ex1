using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    interface ISingleMenuModel : IMenuModel
    {
        string Name { get; set;}
        void Generate(string name, string rows, string cols);
    }
}
