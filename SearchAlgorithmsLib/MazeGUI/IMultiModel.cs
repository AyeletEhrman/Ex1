using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    interface IMultiModel : IModel
    {
        string Name { get; set;}
        string GamesList { get; set;}
    }
}
