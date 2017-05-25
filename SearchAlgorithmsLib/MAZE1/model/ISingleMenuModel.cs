using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.model
{
    interface ISingleMenuModel : IMenuModel
    {
        string Name { get; set;}
        //int Generate(string name, string rows, string cols);
    }
}
