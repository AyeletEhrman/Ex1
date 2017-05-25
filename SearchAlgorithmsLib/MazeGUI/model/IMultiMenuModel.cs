using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.model
{
    interface IMultiMenuModel : IMenuModel
    {
        string Name { get; set;}
        /*string GamesList { get; set;}*/
        string List();
    }
}
